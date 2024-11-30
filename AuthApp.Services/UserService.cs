using AuthApp.Models;
using AuthApp.Services.Repositories;
using AuthApp.Services;
using AuthApp.Utility.Utilities;

namespace AuthApp.Services
{
    public class UserService
    {
        private UserServiceDB _userServiceDB;
        private JWTService _jwtService;

        // Constructor to initialize DB and JWT services
        public UserService(UserServiceDB userServiceDB, JWTService jwtService)
        {
            _userServiceDB = userServiceDB;
            _jwtService = jwtService;
        }

        // Create a new user
        public async Task<dynamic> CreateUser(UserRequest request)
        {
            // Check if user already exists
            var userExist = await _userServiceDB.GetAsync(request.Username);
            if (userExist != null)
            {
                return ("Username " + request.Username + " already exists!");
            }

            // Hash password before saving
            _jwtService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // Create and save new user
            var user = new UserDB
            {
                Username = request.Username,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            await _userServiceDB.CreateAsync(user);
            return user;
        }

        // Log in an existing user
        public async Task<LoggedUser> LoginUser(string username, string Password)
        {
            // Retrieve user from DB
            var user = await _userServiceDB.GetAsync(username);
            if (user is null)
            {
                return null; // User not found
            }

            // Prepare logged user info
            LoggedUser loggedUser = new LoggedUser
            {
                Username = user.Username,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone
            };

            // Verify password hash
            if (!_jwtService.VerifyPasswordHash(Password, user.PasswordHash, user.PasswordSalt))
            {
                return loggedUser; // Incorrect password
            }

            // Generate JWT token
            string token = _jwtService.CreateToken(user);
            loggedUser.Token = token;
            return loggedUser;
        }

        // Get all users
        public async Task<List<User>> GetAllUsers()
        {
            var usersDB = await _userServiceDB.GetAsync();
            var userList = new List<User>();

            // Convert UserDB objects to User model
            foreach (var userDB in usersDB)
            {
                userList.Add(new User
                {
                    Username = userDB.Username,
                    Name = userDB.Name,
                    Email = userDB.Email,
                    Phone = userDB.Phone
                });
            }
            return userList;
        }
    }
}
