using FrisbyBall.Managers;
using FrisbyBall.Models;
using System;
using System.Linq;

namespace FrisbyBall.Controlers
{
    public class LoginPageControler
    {
        private UserManager userManager;
        private User localUser;

        public LoginPageControler()
        {
            userManager = UserManager.DefaultManager;
            Init();
        }

        async void Init()
        {
            Constants.userList = await userManager.GetUsersAsync(); 
        }

        public bool SignInProcedure(string _username, string _password)
        {
            try
            {
                User user = new User
                {
                    UserName = _username,
                    Password = _password
                };

                var LogUser = Constants.userList.FirstOrDefault(_user => user.UserName == _user.UserName
                                                                && user.Password == _user.Password);

                if (LogUser != null)
                {
                    Constants.LocalUser = LogUser;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

    }
}
