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

        // Get all users from the collection
        public async Task<List<UserDB>> GetAsync() => await _userCollection.Find(_ => true).ToListAsync();

        // Get a user by username
        public async Task<UserDB> GetAsync(string username) => await _userCollection.Find(x => x.Username == username).FirstOrDefaultAsync();

        // Insert a new user into the collection
        public async Task CreateAsync(UserDB newUser) => await _userCollection.InsertOneAsync(newUser);

        // Update an existing user by username
        public async Task UpdateAsync(string username, UserDB updatedUser) => await _userCollection.ReplaceOneAsync(x => x.Username == username, updatedUser);

        // Remove a user by username
        public async Task RemoveAsync(string username) => await _userCollection.DeleteOneAsync(x => x.Username == username);
    }
}
