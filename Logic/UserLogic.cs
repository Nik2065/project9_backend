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
        }

        DataAccess.DataContext _db;

        public void CreateNewSiteUser(string userEmail, string userPwd, SiteRoleEnum role)
        {
            //проверяем что пользователь уникален
            var item = _db.SiteUsers.FirstOrDefault(item => item.EmailAsLogin == userEmail.Trim());
            if (item != null)
                throw new Exception("Пользователь с таким email уже зарегистирован");


            var u = new SiteUserDb
            {
                EmailAsLogin = userEmail,
                Password = HashPassword(userPwd),
                Created = DateTime.Now,
                SiteRoleId = (int)role
            };


            _db.SiteUsers.Add(u);
            _db.SaveChanges();
        }

        public string HashPassword(string password)
        {
            // Generate a 128-bit salt using a sequence of
            // cryptographically strong random bytes.
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }


        public ClaimsIdentity GetIdentity(string emailAsLogin, string password)
        {
            var h = HashPassword(password);
            var _users = new List<SiteUserDb>();
            SiteUserDb user = _users.FirstOrDefault(x => x.EmailAsLogin == emailAsLogin && x.Password == h);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.EmailAsLogin),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.SiteRoleId.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }


    }
}
