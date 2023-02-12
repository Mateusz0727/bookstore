using Bookshop.App.Models.Auth;
using Bookshop.App.Services.User;
using Bookshop.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace Bookshop.App.Services.Auth
{
    public class AuthService
    {
        protected  UserService _userService;

        protected IPasswordHasher<Data.Model.User> Hasher { get; }


        public AuthService(IPasswordHasher<Data.Model.User> hasher,UserService userService)
        {
            Hasher = hasher;
            _userService = userService;
        }
        public bool Login(LoginFormModel login, Data.Model.User user)
        {
            var result = Hasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);
            if (result == PasswordVerificationResult.Success)
            {
                return true;
            }

            return false;
        }

     
    }
}
