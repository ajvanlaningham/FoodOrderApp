using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using System.Threading.Tasks;
using System.Linq;
using FoodOrderApp.Models;

namespace FoodOrderApp.Services
{
    public class UserService
    {
        FirebaseClient client;

        public UserService()
        {
            client = new FirebaseClient("https://posv1-da974-default-rtdb.firebaseio.com/");
        }

        public async Task<bool> IsUserExist(string uname)
        {
            var user = (await client.Child("Users")
                .OnceAsync<User>()).Where(u => u.Object.Username == uname).FirstOrDefault();

            return (user != null);
        }

        public async Task<bool> RegisterUser(string uname, string passwd)
        {
            if  (await IsUserExist(uname) == false)
            {
                await client.Child("Users")
                    .PostAsync(new User()
                    {
                        Username = uname,
                        Password = passwd
                    });
            }
        }
    }
}
