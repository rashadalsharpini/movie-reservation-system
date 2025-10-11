using Shared.Dtos;

namespace ServiceAbstraction;

public interface IAuthenticationService
{
    Task<UserDto> LoginAsync(LoginDto dto);
    Task<UserDto> RegisterAsync(RegisterDto dto);
    Task ForgotPasswordAsync(ForgotPasswordDto dto);
    Task ResetPasswordAsync(ResetPasswordDto dto);
}