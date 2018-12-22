using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        public Seed(DataContext context)
        {
            this.context = context;
        }

        public void SeedUsers()
        {
            string userData = File.ReadAllText("Data/UserSeedData.json");
            List<User> users = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach (User user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.UserName = user.UserName.ToLower();

                context.Users.Add(user);
            }

            context.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private readonly DataContext context;
    }
}