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
            _jwtService = jwtService;
        }

        // Validate new user registration data
        public string ValidateNew(UserRequest request)
        {
            var emailValidation = new EmailAddressAttribute();

            // Check if the email is valid
            if (!emailValidation.IsValid(request.Email))
            {
                return "Email Address is Invalid";
            }
            // Check if the phone number is exactly 10 digits
            else if (request.Phone.ToString().Length != 10)
            {
                return "Phone Number is Invalid";
            }

            return null; // No validation errors
        }

        // Validate existing user credentials
        public string ValidateExistingUser(LoggedUser user)
        {
            // Check if user exists
            if (user is null)
            {
                return "Username does not exist!";
            }
            // Check if token is null (incorrect password or not authenticated)
            if (user.Token == null)
            {
                return "Incorrect Password!";
            }

            return null; // No validation errors
        }
    }
}
