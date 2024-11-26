using System.ComponentModel.DataAnnotations;

namespace AuthApp.Services.Config
{
	public class DatabaseSettings
	{
		[Required(ErrorMessage = "The database connection string is required.")]
		[Url(ErrorMessage = "The connection string must be a valid URL.")]
		public string ConnectionString { get; set; } = string.Empty;

		[Required(ErrorMessage = "The database name is required.")]
		[MaxLength(50, ErrorMessage = "The database name cannot exceed 50 characters.")]
		public string DatabaseName { get; set; } = string.Empty;
	}
}
