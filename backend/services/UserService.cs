using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniSocial.Data;
using MiniSocial.Dto.User;
using MiniSocial.Models;

namespace MiniSocial.Services;

public class UserService(IPasswordHasher<User> passwordHasher, AppDbContext context)
{
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly AppDbContext _context = context;

    public async Task<List<UserResponseDto>> GetUsers()
    {
        //pega todos os usuarios e cria uma lista mapeando pra UserResponseDto
        return await _context.Users.AsNoTracking().Select(post => new UserResponseDto(
            post.Id,
            post.UserName,
            post.CreatedAt,
            post.UpdatedAt
        ))
        .ToListAsync();
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto login)
    {
        //procura o usuario pelo UserName. se nao achar da exception
        User user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.UserName == login.UserName) ?? throw new Exception("Failed Login");

        //confere se a senha ta certa. se errada da exception
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, login.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new Exception("Failed Login");
        } 

        return new LoginResponseDto(user.Id, user.UserName); 
    }

    public async Task<UserResponseDto> PostUser(UserRequestDto userRequest)
    {
        //cria o usuario no tipo User pra ir pro banco
        User user = new(userRequest.UserName, "", null);

        //usa o passwordhasher do asp.net core pra dar hash na senha
        user.Password = _passwordHasher.HashPassword(user, userRequest.Password);

        //adiciona no banco
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserResponseDto(user.Id, user.UserName, user.CreatedAt, null);
    }

    public async Task<UserResponseDto> UpdateUser(UserUpdateDto userUpdate)
    {   
        //confere se tem algo pra atualizar
        if (userUpdate.UserName == null && userUpdate.Password == null && userUpdate.Bio == null) throw new Exception("Nothing to update");

        //procura o usuario pelo Id. se nao achar da exception
        User user = await GetUserById(userUpdate.Id);
        
        //atualiza o que tem que atualizar
        if (!string.IsNullOrWhiteSpace(userUpdate.UserName)) user.UserName = userUpdate.UserName; 
        if (!string.IsNullOrWhiteSpace(userUpdate.Password)) user.Password = _passwordHasher.HashPassword(user, userUpdate.Password);
        if (!string.IsNullOrWhiteSpace(userUpdate.Bio)) user.Bio = userUpdate.Bio;

        //salva a data que foi editado
        user.UpdatedAt = DateTime.UtcNow;

        //salva no banco
        await _context.SaveChangesAsync();

        return new UserResponseDto(user.Id, user.UserName, user.CreatedAt, user.UpdatedAt);
    }

    public async Task<User> GetUserById (Guid id)
    {
        //procura o usuario com o id que foi passado se nao existir da exceptions
        User user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id) ?? throw new Exception("Id not found");
        return user;
    }

    public async Task DeleteUser (UserDeleteDto userDelete)
    {
        //remove o usuario do banco
        User user = await GetUserById(userDelete.Id);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}

