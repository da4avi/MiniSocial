using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniSocial.Data;
using MiniSocial.Dto;
using MiniSocial.Models;

namespace MiniSocial.Services;

public class UserService
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AppDbContext _context;

    public UserService(IPasswordHasher<User> passwordHasher, AppDbContext context)
    {
        _passwordHasher = passwordHasher;
        _context = context;
    }

    //mock
    private static readonly List<User> _users = [];

    public async Task<List<User>> GetAllUsers()
    {
        List<User> users = await _context.Users.ToListAsync();

        return users;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto login)
    {
        //procura o usuario pelo UserName. se nao achar da exception
        User user = _users.FirstOrDefault(user => user.UserName == login.UserName) ?? throw new Exception("Failed Login");

        //confere se a senha ta certa. se erradi exception
        var result = _passwordHasher.VerifyHashedPassword(null!, user.Password, login.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new Exception("Failed Login");
        } 

        return new LoginResponseDto(user.Id, user.UserName); 
    }

    public async Task<UserResponseDto> PostUser(UserRequestDto userRequest)
    {
        //usa o passwordhasher do asp.net core pra dar hash na senha
        string passwordHash = _passwordHasher.HashPassword(null!, userRequest.Password);

        //cria o usuario no tipo User pra ir pro banco
        User user = new(userRequest.UserName, passwordHash, userRequest.Password);

        //adiciona no banco
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        //adiciona no mock
        _users.Add(user);

        return new UserResponseDto(user.Id, user.UserName, user.CreatedAt, null);
    }

    public async Task<UserResponseDto> UpdateUser(UserUpdateDto userUpdate)
    {   
        //confere se tem algo vim pra atualizar
        if (userUpdate.UserName == null && userUpdate.Password == null && userUpdate.Bio == null) throw new Exception("Nothing to update");

        //procura o usuario pelo Id. se nao achar da exception
        User user = await GetUserById(userUpdate.Id);
        
        //atualiza o que tem que atualizar
        if (!string.IsNullOrWhiteSpace(userUpdate.UserName)) user.UserName = userUpdate.UserName;
        if (!string.IsNullOrWhiteSpace(userUpdate.Password)) user.Password = _passwordHasher.HashPassword(null!, userUpdate.Password);
        if (!string.IsNullOrWhiteSpace(userUpdate.Bio)) user.Bio = userUpdate.Bio;

        //salva a data que foi editado
        user.UpdatedAt = DateTime.UtcNow;

        return new UserResponseDto(user.Id, user.UserName, user.CreatedAt, user.UpdatedAt);
    }

    public async Task<User> GetUserById (Guid id)
    {
        User user = _users.FirstOrDefault(user => user.Id == id) ?? throw new Exception("Id not found");
        return user;
    }
}

