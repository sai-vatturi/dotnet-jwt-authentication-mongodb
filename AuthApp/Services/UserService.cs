using AuthApp.Models;
using AuthApp.Repositories;
using AuthApp.Services;

namespace AuthApp.Services
{
	public class UserService
	{
		private UserServiceDB _userServiceDB;
        private JWTService _jwtService;
        public UserService(UserServiceDB userServiceDB, JWTService jwtService)
        {
            _userServiceDB = userServiceDB;
            _jwtService=jwtService;
        }

		public async Task<dynamic> CreateUser(UserRequest request)
        {
            var userExist = await _userServiceDB.GetAsync(request.Username);
            if (userExist != null)
            {
                return ("Username " + request.Username + " already exist!");
            }
            _jwtService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
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

		public async Task<LoggedUser> LoginUser(string username, string Password)
        {
            var user = await _userServiceDB.GetAsync(username);
            if (user is null)
            {
                return null;
            }
            LoggedUser loggedUser = new LoggedUser
            {
                Username = user.Username,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone
            };
            if (!_jwtService.VerifyPasswordHash(Password, user.PasswordHash, user.PasswordSalt))
            {
                return loggedUser;
            }
            string token = _jwtService.CreateToken(user);
            loggedUser.Token = token;
            return loggedUser;
        }

		public async Task<List<User>> GetAllUsers()
        {
            var usersDB = await _userServiceDB.GetAsync();
            var userList = new List<User>();
            foreach (var userDB in usersDB)
            {
                userList.Add(new User
                {
                    Username = userDB.Username,
                    Name = userDB.Name,
                    Email = userDB.Email,
                    Phone = userDB.Phone
                }
                );
            }
            return userList;
        }
	}
}
