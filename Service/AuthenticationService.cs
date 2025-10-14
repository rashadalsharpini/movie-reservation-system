using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Shared.Dtos;

namespace Service;

public class AuthenticationService(
    UserManager<User> userManager,
    IConfiguration configuration,
    IEmailService emailService,
    TokenProvider tokenProvider)
    : IAuthenticationService
{
    public async Task<UserDto> LoginAsync(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email) ??
                   throw new Exception("Invalid email");
        var isPassword = await userManager.CheckPasswordAsync(user, dto.Password);
        if (!isPassword) throw new Exception("Invalid password");
        user.RefreshTokens = user.RefreshTokens.Where(t => t.Expires >= DateTime.UtcNow).ToList();
        var existingToken = user.RefreshTokens.FirstOrDefault(t => t.DeviceId == dto.DeviceId);
        if (existingToken != null) user.RefreshTokens.Remove(existingToken);
        var accessToken = await tokenProvider.GenerateAccessToken(user);
        var refreshToken = tokenProvider.GenerateRefreshToken();
        var refresh = new RefreshToken()
        {
            Token = refreshToken,
            Expires = DateTime.UtcNow.AddHours(1),
            UserId = user.Id,
            DeviceId = dto.DeviceId
        };
        user.RefreshTokens.Add(refresh);
        await userManager.UpdateAsync(user);
        return new UserDto()
        {
            DisplayName = user.DisplayName,
            Email = user.Email!,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };
    }


    public async Task<UserDto> RegisterAsync(RegisterDto dto)
    {
        var user = new User()
        {
            DisplayName = dto.DisplayName,
            Email = dto.Email,
            UserName = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };
        var res = await userManager.CreateAsync(user, dto.Password);
        if (!res.Succeeded) throw new Exception(res.Errors.First().Description);
        return new UserDto()
        {
            DisplayName = dto.DisplayName,
            Email = dto.Email,
            AccessToken = await tokenProvider.GenerateAccessToken(user)
        };
    }

    public async Task ForgotPasswordAsync(ForgotPasswordDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email) ?? throw new Exception("Invalid email");
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = System.Web.HttpUtility.UrlEncode(token);
        var link = $"{configuration["AppSettings:ClientURL"]}/resetpassword?email={dto.Email}&token={encodedToken}";
        var body = $@"
                      <h2>Hello {user.DisplayName},</h2>
                      <p>You have requested to reset your password. Please click the link below to proceed:</p>
                      <p><a href='{link}' click to reset password</p>
                      <p>if you didn't request this, you can safely ignore this email.</p>";
        await emailService.SendEmailAsync(user.Email!, "Reset Password", body);
    }

    public async Task ResetPasswordAsync(ResetPasswordDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email) ?? throw new Exception("Invalid email");
        var decodedToken = System.Web.HttpUtility.UrlDecode(dto.Token);
        var res = await userManager.ResetPasswordAsync(user, decodedToken!, dto.NewPassword);
        if (!res.Succeeded) throw new Exception(res.Errors.First().Description);
    }

    public async Task<UserDto> RefreshTokenAsync(string refreshToken)
    {
        var user = userManager.Users
                       .Include(u => u.RefreshTokens)
                       .FirstOrDefault(u => u.RefreshTokens.Any(t => t.Token == refreshToken))
                   ?? throw new Exception("Invalid refresh token");

        var existingToken = user.RefreshTokens.First(t => t.Token == refreshToken);

        if (existingToken.Expires < DateTime.UtcNow)
            throw new Exception("Refresh token expired");

        user.RefreshTokens.Remove(existingToken);

        var newAccessToken = await tokenProvider.GenerateAccessToken(user);
        var newRefreshToken = tokenProvider.GenerateRefreshToken();

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            Expires = DateTime.UtcNow.AddDays(7),
            UserId = user.Id
        });

        await userManager.UpdateAsync(user);

        return new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email!,
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    public async Task RevokeRefreshTokenAsync(string refreshToken)
    {
        var user = await userManager.Users.Include(u => u.RefreshTokens)
                       .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken)) ??
                   throw new Exception("Invalid refresh token");
        var token = user.RefreshTokens.First(t => t.Token == refreshToken);
        user.RefreshTokens.Remove(token);
        await userManager.UpdateAsync(user);
    }
}