
# AuthApp - JWT Authentication Service

This project is a simple JWT-based authentication system built using ASP.NET Core. It provides secure user login and registration functionality.

## Features
- User Registration
- User Login
- JWT Token Generation
- Password Hashing and Verification

## Prerequisites
1. .NET 6 SDK or higher installed on your system.
2. MongoDB as the database for storing user data.

## How to Run
1. Clone this repository:
   ```bash
   git clone https://github.com/sai-vatturi/dotnet-jwt-authentication-mongodb.git
   ```
2. Navigate to the project directory:
   ```bash
   cd dotnet-jwt-authentication-mongodb
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Update your `appsettings.json` with the correct database connection string and a secure token:
   ```json
   {
       "AppSettings": {
           "Token": "YourSecureTokenKey12345!"
       },
       "Database": {
           "ConnectionString": "your-mongodb-connection-string",
           "DatabaseName": "YourDatabaseName"
       }
   }
   ```
5. Run the application:
   ```bash
   dotnet run
   ```
6. Open your browser and visit:
   - Development: `http://localhost:5025/swagger`

## API Endpoints
### User Registration
- **POST** `/user/register`
- Headers:
  - `Username`: string
  - `Password`: string
  - `Email`: string
  - `Name`: string
  - `Phone`: long

### User Login
- **POST** `/user/login`
- Headers:
  - `Username`: string
  - `Password`: string

## Tech Stack
- **Backend:** ASP.NET Core
- **Database:** MongoDB
- **Authentication:** JWT

## Contributing
Feel free to open issues or submit pull requests. Contributions are welcome!
