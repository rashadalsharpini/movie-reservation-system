using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.Dtos;

namespace Service;

public class AuthenticationService(
    UserManager<User> userManager,
    IConfiguration configuration,
    IMapper mapper,
    IEmailService emailService)
    : IAuthenticationService
{
    public async Task<UserDto> LoginAsync(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email) ??
                   throw new Exception("Invalid email or password");
        var isPassword = await userManager.CheckPasswordAsync(user, dto.Password);
        if (isPassword)
            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateToken(user)
            };
        throw new Exception("Invalid email or password");
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
            Token = await GenerateToken(user)
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

    private async Task<string> GenerateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };
        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));
        var secretKey = configuration["JWTSettings:SecretKey"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: configuration["JWTSettings:Issuer"],
            audience: configuration["JWTSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}