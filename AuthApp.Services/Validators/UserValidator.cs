using System.ComponentModel.DataAnnotations;
using AuthApp.Models;
using AuthApp.Services.Repositories;
using AuthApp.Services;
using AuthApp.Utility.Utilities;

namespace AuthApp.Services.Validators
{
	public class UserValidator
	{
		private UserServiceDB _userServiceDB;
        private UserService _userService;
        private JWTService _jwtService;
        public UserValidator(UserServiceDB userServiceDB, UserService userService, JWTService jwtService)
        {
            _userServiceDB = userServiceDB;
            _userService = userService;
            _jwtService=jwtService;
        }

        public string ValidateNew(UserRequest request)
        {
            var emailValidation = new EmailAddressAttribute();
            if (!emailValidation.IsValid(request.Email))
            {
                return "Email Address is Invalid";
            }
            else if (request.Phone.ToString().Length != 10)
            {
                return "Phone Number is Invalid";
            }
            return null;
        }

        public string ValidateExistingUser(LoggedUser user)
        {
            if (user is null)
            {
                return "Username does not exist!";
            }
            if (user.Token == null)
            {
                return "Incorrect Password!";
            }
            return null;
        }
    }
}
