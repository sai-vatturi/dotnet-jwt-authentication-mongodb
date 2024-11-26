using MongoDB.Bson.Serialization.Attributes;

namespace AuthApp.Models
{
	public class User
	{
		public string Username {get; set;}
		public string Name {get; set;}
		public string Email {get; set;}
		public long Phone {get; set;}

	}

	public class UserDB : User
	{
		public byte[] PasswordHash {get; set;}
		public byte[] PasswordSalt {get; set;}

		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public string Id {get; internal set;}
	}

	public class LoggedUser : User
	{
		public string Token {get; set;}

	}

	public class UserRequest : User
	{
		public string Password {get; set;}
	}
}
