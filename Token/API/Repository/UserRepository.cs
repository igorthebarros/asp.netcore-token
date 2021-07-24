using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public static class UserRepository
    {
        //Simulate a BD
        public static User Get(string username, string password)
        {
            var user = new List<User>();

            user.Add(new User { Id = 1, Username = "lona", Password = "lona", Role = "manager" });
            user.Add(new User { Id = 1, Username = "hume", Password = "hume", Role = "employee" });

            return user.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}