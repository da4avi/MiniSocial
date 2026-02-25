using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using MiniSocial.Dto;
using MiniSocial.Models;

namespace MiniSocial.Services;

public class UserService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    //mock
    private static List<User> _users = new List<User> {};

    public async Task<List<User>> GetAllUsers()
    {
        await Task.Delay(100);

        return _users;
    }

    public async Task<UserResponseDto> PostUser(UserRequestDto userEntry)
    {

        if (userEntry.password.Length < 10)
        {
            await Task.Delay(100);
            throw new ArgumentException("a senha precisa ter mais de 10 caracteres");
        }

        User user = new User
        {
          userName = userEntry.userName,
        };

        //usa o passwordhasher do asp.net core pra dar hash na senha
        user.password = _passwordHasher.HashPassword(user, userEntry.password);

        _users.Add(user);
        await Task.Delay(100);

        UserExitDto userExit = new UserExitDto
        {
            userName = user.userName
        };

        return userExit;
    }
}

