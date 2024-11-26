using AuthApp.Services.Config;
using AuthApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuthApp.Services.Repositories
{
	public class UserServiceDB
	{
		private readonly IMongoCollection<UserDB> _userCollection;

		public UserServiceDB(IOptions<DatabaseSettings> userDatabaseSettings)
		{
			var mongoClient = new MongoClient(userDatabaseSettings.Value.ConnectionString);
			var mongoDatabase = mongoClient.GetDatabase(userDatabaseSettings.Value.DatabaseName);
			_userCollection = mongoDatabase.GetCollection<UserDB>("Users");
		}

		public async Task<List<UserDB>> GetAsync() => await _userCollection.Find(_ => true).ToListAsync();
		public async Task<UserDB> GetAsync(string username) => await _userCollection.Find(x => x.Username == username).FirstOrDefaultAsync();
		public async Task CreateAsync(UserDB newUser) => await _userCollection.InsertOneAsync(newUser);
		public async Task UpdateAsync(string username, UserDB updatedUser) => await _userCollection.ReplaceOneAsync(x => x.Username == username, updatedUser);
		public async Task RemoveAsync(string username) => await _userCollection.DeleteOneAsync(x => x.Username == username);

	}
}
