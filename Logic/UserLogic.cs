using DataAccess.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Logic
{
    public class UserLogic
    {
        public UserLogic(DataAccess.DataContext db)
        { 
            _db = db;
            _logger = NLog.LogManager.GetCurrentClassLogger();
        }

        private DataAccess.DataContext _db;
        private NLog.Logger _logger;

        public void CreateNewSiteUser(string userEmail, string userPwd, SiteRoleEnum role)
        {
            //проверяем что пользователь уникален
            var item = _db.SiteUsers.FirstOrDefault(item => item.EmailAsLogin == userEmail.Trim());
            if (item != null)
                throw new Project9CustomException("Пользователь с таким email уже зарегистирован");

            var hr = CreateHashedPassword(userPwd);
            var u = new SiteUserDb
            {
                EmailAsLogin = userEmail,
                Password = hr.Hash,
                Salt = hr.Base64Salt,
                Created = DateTime.Now,
                SiteRoleId = (int)role
            };


            _db.SiteUsers.Add(u);
            _db.SaveChanges();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        internal string GetHashedPassword(string passwordAsText, string salt)
        {
            var saltInBytes = Convert.FromBase64String(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwordAsText!,
                salt: saltInBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }


        internal HashResult CreateHashedPassword(string password)
        {
            var result = new HashResult();
            // Generate a 128-bit salt using a sequence of
            // cryptographically strong random bytes.
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            result.Base64Salt = Convert.ToBase64String(salt);

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            result.Hash = hashed;
            return result;
        }


        public ClaimsIdentity GetIdentity(string emailAsLogin, string passwordAsText)
        {

            try
            {
                //var _users = new List<SiteUserDb>();
                SiteUserDb user = _db.SiteUsers.FirstOrDefault(x => x.EmailAsLogin == emailAsLogin);



                if (user != null)
                {
                    //проверяем пароль

                    if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Salt))
                        throw new Exception("Для пользователя " + emailAsLogin + " не найдены данные о пароле");

                    var hash1 = user.Password;
                    var hash2 = GetHashedPassword(passwordAsText, user.Salt);
                    
                    if (hash1 == hash2)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimsIdentity.DefaultNameClaimType, user.EmailAsLogin),
                            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.SiteRoleId.ToString()),
                        };
                        ClaimsIdentity claimsIdentity =
                        new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                            ClaimsIdentity.DefaultRoleClaimType);
                        return claimsIdentity;
                    }
                }

            }
            catch(Exception ex) 
            {
                _logger.Error(ex);
            }

            // если пользователя не найдено
            return null;
        }


    }
}
