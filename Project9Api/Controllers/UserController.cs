using DataAccess.Entities;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project9Api.ApiDto;
using Project9Api.Auth;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Project9Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public UserController(IConfiguration config)
        {
            //_users = new List<SiteUserDb>();
            //_users.Add(new SiteUserDb { EmailAsLogin = "pro@pro.ru", Password = "123456", SiteRoleId = 1 });
            //Config = config;
            var conn = config["MainSettings:DbConnectionString"];
            var db = new DataAccess.DataContext(conn);
            _uLogic = new UserLogic(db);
            _logger = NLog.LogManager.GetCurrentClassLogger();
        }

        //private readonly IConfiguration Config;

        private UserLogic _uLogic;
        private NLog.Logger _logger;

        //private List<SiteUserDb> _users; 

        //
        // регистрация пользователя
        //
        [HttpPost]
        [Route("[action]")]
        public ActionResult<SignUpResponse> SignUp(SignUpRequest request)
        {

            var result = new SignUpResponse { IsError = false, Message = $"Пользователь с логином:{request.Email} зарегистирован"};


            try
            {
                //todo: проверки
                _uLogic.CreateNewSiteUser(request.Email, request.Password, SiteRoleEnum.SimpleUser);
            }
            catch(Project9CustomException ex)
            {
                var m = "Ошибка при создании пользователя:" + ex.Message;//TODO
                result.IsError = true;
                result.Message = m;
                _logger.Error(m, ex);
            }
            catch (Exception ex)
            { 
                var m = "Ошибка при создании пользователя";
                result.IsError = true;
                result.Message = m;
                _logger.Error(m, ex);
            }


            return Ok(result);
        }



        //
        // Получение токена для входа в аккаунт
        //
        [HttpPost]
        [Route("[action]")]
        public ActionResult<TokenResponse> Token(TokenRequest request)
        {
            var result = new TokenResponse();


            try
            {
                var identity = _uLogic.GetIdentity(request.EmailAsLogin, request.Pwd);
                if (identity == null)
                {
                    return BadRequest(new TokenResponse { IsError = true, Message = "Invalid username or password." }) ;
                }

                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                /*var response = new
                {
                    access_token = encodedJwt,
                    username = identity.Name
                };*/

                //return Json(response);

                result.AccessToken = encodedJwt;
                result.Login = identity.Name;
                result.ExpiresIn = now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)).ToString("r");

            }
            catch (Exception ex)
            {
                var m = "Ошибка получения токена ";
                _logger.Error(m +": " + ex);
                result.IsError = true;
                result.Message = m;
            }


            return Ok(result);
        }




        //[HttpPost]
        //[Route("[action]")]

    }

    public class TokenRequest
    {
        public string EmailAsLogin { get; set; }
        public string Pwd { get; set; }
    }
}
