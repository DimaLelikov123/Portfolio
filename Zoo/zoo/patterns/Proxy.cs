using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace zootopia
{
    public interface IUserAuthorization
    {
        bool Login(string username, string password);
    }

    public class UserAuthorization : IUserAuthorization
    {
        private string username = "admin";
        private string password = "12345";
        public bool Login(string username, string password)
        {
            return username == this.username && password == this.password;
        }
    }

    public class UserAuthorizationProxy : IUserAuthorization
    {
        private int authorizationAttempts = 0;
        private UserAuthorization realSubject;

        public bool Login(string username, string password)
        {
            if (authorizationAttempts == 3)
            {
                MessageBox.Show("Your authorization attempts have expired.");
                Application.Current.Shutdown();
            }

            realSubject = new UserAuthorization();
            authorizationAttempts++;
            bool check = realSubject.Login(username, password);
            if (!check)
            {
                MessageBox.Show($"Incorrect login and/or password. Remaining attempts: {4 - authorizationAttempts}");
            }
            return check;
        }
    }

    public class User
    {
        private IUserAuthorization authorization;

        public User(IUserAuthorization authorization)
        {
            this.authorization = authorization;
        }

        public bool LoginUser(string username, string password)
        {
            bool isValid = authorization.Login(username, password);
            return isValid;
        }
    }
}
