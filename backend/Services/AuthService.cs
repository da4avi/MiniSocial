using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using MiniSocial.DTOs.Auth;
using MiniSocial.Models;

namespace MiniSocial.Services;

public class AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ProfileService profileService)
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly ProfileService _profileService = profileService;

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
    {
        //tenta logar
        var result = await _signInManager.PasswordSignInAsync(
        loginRequest.UserName,
        loginRequest.Password,
        isPersistent: true,
        lockoutOnFailure: true
        );

        return result.Succeeded ? new LoginResponseDto(result.Succeeded, null) : new LoginResponseDto(result.Succeeded, "Login Failed");
    }

    public async Task Logout()
    {
        //desloga
        await _signInManager.SignOutAsync();
    }

    public async Task<RegisterResponseDto> Register(RegisterRequestDto registerRequest)
    {
        //cria o usuario no tipo User pra ir pro banco
        var user = new IdentityUser
        {
            UserName = registerRequest.UserName,
            Email = registerRequest.Email
        };

        //tenta adicionar no banco
        var result = await _userManager.CreateAsync(user, registerRequest.Password);


        if (result.Succeeded)
        {
            //cria o profile se deu certo
            await _profileService.RegisterProfile(registerRequest, user.Id);
            return new RegisterResponseDto(result.Succeeded, null);
        }
        else
        {
            return new RegisterResponseDto(result.Succeeded, result.Errors.Select(e => e.Description));
        }
    }

    public async Task<UserUpdateResponseDto> UpdateUser(UserUpdateRequestDto userUpdateRequest, string userId)
    {
        //procura o IdentityUser
        IdentityUser user = await _userManager.FindByIdAsync(userId) ?? throw new KeyNotFoundException($"User with ID {userId} not Found");

        //se o cara quiser mudar username ele tenta se nao conseguir devolve erro
        if (!string.IsNullOrWhiteSpace(userUpdateRequest.UserName))
        {
            var result = await _userManager.SetUserNameAsync(user, userUpdateRequest.UserName);

            if (!result.Succeeded) return new UserUpdateResponseDto(result.Succeeded, result.Errors.Select(e => e.Description));
        }

        //se o cara quiser mudar a senha ele tenta se nao conseguir devolve erro
        if (!string.IsNullOrWhiteSpace(userUpdateRequest.CurrentPassword) && !string.IsNullOrWhiteSpace(userUpdateRequest.NewPassword))
        {
            var result = await _userManager.ChangePasswordAsync(user, userUpdateRequest.CurrentPassword, userUpdateRequest.NewPassword);

            if (!result.Succeeded) return new UserUpdateResponseDto(result.Succeeded, result.Errors.Select(e => e.Description));
        }

        //atualiza o profile com username e bio
        await _profileService.UpdateProfile(userUpdateRequest, user.Id);
        //da refresh no login pra atualizar
        await _signInManager.RefreshSignInAsync(user);
        
        return new UserUpdateResponseDto(true, null);
        // if (!string.IsNullOrWhiteSpace(userUpdateRequest.Email)) _userManager.ChangeEmailAsync(user, userUpdateRequest.Email, );
    }
}

