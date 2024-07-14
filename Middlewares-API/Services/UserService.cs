using Middlewares_API.Models.Entity;

namespace Middlewares_API.Services;

public static class UserService
{
    private readonly static List<User> _users = new List<User>
    {
        new User { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" },
        new User { Id = 2, Username = "user", Password = "user123", Role = "User" }
    };

    public static List<User> GetUsers()
    {
        return _users;
    }
    
    public static User GetUserById(int id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }
    
    public static User GetUser(string userName, string password)
    {
        return _users.FirstOrDefault(x => x.Username == userName && x.Password == password);
    }
}