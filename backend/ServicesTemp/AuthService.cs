using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using MiniSocial.Dto.Auth;

namespace MiniSocial.Services;

public class AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;

    public async Task<LoginResponseDto> Login(LoginRequest loginRequest)
    {
        var result = await _signInManager.PasswordSignInAsync(
        loginRequest.Email,
        loginRequest.Password,
        isPersistent: true,
        lockoutOnFailure: true
        );

        return result.Succeeded ? new LoginResponseDto(result.Succeeded, null) : new LoginResponseDto(result.Succeeded, "Login Failed");
    }

    public async Task<RegisterResponseDto> Register(RegisterRequest registerRequest)
    {
        //cria o usuario no tipo User pra ir pro banco
        var user = new IdentityUser
        {
            UserName = registerRequest.Email,
            Email = registerRequest.Email
        };

        var result = await _userManager.CreateAsync(user, registerRequest.Password);

        return result.Succeeded ? new RegisterResponseDto(result.Succeeded, null) : new RegisterResponseDto(result.Succeeded, result.Errors.Select(e => e.Description));
    }

    // public async Task<UserResponseDto> UpdateUser(UserUpdateDto userUpdate)
    // {   
    //     //confere se tem algo pra atualizar
    //     if (userUpdate.UserName == null && userUpdate.Password == null && userUpdate.Bio == null) throw new Exception("Nothing to update");

    //     //procura o usuario pelo Id. se nao achar da exception
    //     UserProfile user = await GetUserById(userUpdate.Id);

    //     //atualiza o que tem que atualizar
    //     if (!string.IsNullOrWhiteSpace(userUpdate.UserName)) user.UserName = userUpdate.UserName; 
    //     if (!string.IsNullOrWhiteSpace(userUpdate.Password)) user.Password = _passwordHasher.HashPassword(user, userUpdate.Password);
    //     if (!string.IsNullOrWhiteSpace(userUpdate.Bio)) user.Bio = userUpdate.Bio;

    //     //salva a data que foi editado
    //     user.UpdatedAt = DateTime.UtcNow;

    //     //salva no banco
    //     await _context.SaveChangesAsync();

    //     return new UserResponseDto(user.Id, user.UserName, user.CreatedAt, user.UpdatedAt);
    // }

    // public async Task<UserProfile> GetUserById (Guid id)
    // {
    //     //procura o usuario com o id que foi passado se nao existir da exceptions
    //     UserProfile user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id) ?? throw new Exception("Id not found");
    //     return user;
    // }

    // public async Task DeleteUser (UserDeleteDto userDelete)
    // {
    //     //remove o usuario do banco
    //     UserProfile user = await GetUserById(userDelete.Id);
    //     _context.Users.Remove(user);
    //     await _context.SaveChangesAsync();
    // }
}

