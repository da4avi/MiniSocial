using System.Reflection;
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

    private static List<User> _users = new List<User> {};

    public async Task<List<User>> GetAllUsers()
    {
        await Task.Delay(100);

        return _users;
    }

    public async Task<UserExitDto> PostUser(UserEntryDto user)
    {

        if (user.password.Length < 10)
        {
            await Task.Delay(100);
            throw new ArgumentException("a senha precisa ter mais de 10 caracteres");
        }

        User userEntry = new User
        {
          userName = user.userName,
        };

        //usa o passwordhasher do asp.net core pra dar hash na senha
        userEntry.password = _passwordHasher.HashPassword(userEntry, user.password);

        _users.Add(userEntry);
        await Task.Delay(100);

        UserExitDto userExit = new UserExitDto
        {
            userName = userEntry.userName
        };

        return userExit;
    }
}

