using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AuthApp.Services;
using AuthApp.Models;
using AuthApp.Services.Validators;

namespace AuthApp.API.Controllers
{
    [Tags("Users")]
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserService _userService;
        private UserValidator _userValidator;

        public AuthController(UserValidator userValidator, UserService userService)
        {
            _userService = userService;
            _userValidator = userValidator;
        }

        // Register a new user
        [HttpPost("register")]
        public async Task<ActionResult<UserDB>> Register(
            [FromHeader, BindRequired] string Username, [FromHeader, BindRequired] string Password,
            [FromHeader] string Email, [FromHeader] string Name,
            [FromHeader] long Phone)
        {
            // Create a user request object
            UserRequest request = new()
            {
                Username = Username,
                Password = Password,
                Email = Email,
                Name = Name,
                Phone = Phone
            };

            // Validate the user input
            var inValid = _userValidator.ValidateNew(request);
            if (inValid != null)
            {
                return BadRequest(inValid); // Return bad request if validation fails
            }

            // Create the user
            var user = await _userService.CreateUser(request);
            UserDB instance = user as UserDB;
            if (instance is null)
            {
                return BadRequest(user); // Return bad request if user creation fails
            }
            return Ok(user); // Return OK response with created user
        }

        // User login
        [HttpGet("login")]
        public async Task<ActionResult<LoggedUser>> Login(
            [FromHeader, BindRequired] string Username, [FromHeader, BindRequired] string Password)
        {
            // Authenticate user and get logged user data
            var user = await _userService.LoginUser(Username, Password);

            // Validate user credentials
            var inValid = _userValidator.ValidateExistingUser(user);
            if (inValid != null)
            {
                return BadRequest(inValid); // Return bad request if login fails
            }
            return Ok(user); // Return OK with logged user info and token
        }

        // Get the list of all users
        [HttpGet("UsersList")]
        public async Task<ActionResult> GetAll()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users); // Return OK with list of users
        }
    }
}
