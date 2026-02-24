using MiniSocial.Models;

namespace MiniSocial.Services;

public class UserService
{
    private static List<User> _users = new List<User>
    {
        new User { Id = 1, userName = "davi", password = "123" },
        new User { Id = 2, userName = "rau", password = "456" }
    };

    public async Task<List<User>> GetAllUsers()
    {
        await Task.Delay(100);

        return _users;
    }

    public async Task<User> PostUser(User user)
    {
        try
        {
            if (user.password.Length < 10)
            {
                await Task.Delay(100);
                throw new Exception("a senha precisa ter mais de 10 caracteres");
            }

            _users.Add(user);
            await Task.Delay(100);

            return user;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

