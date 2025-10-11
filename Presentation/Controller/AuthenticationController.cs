using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos;

namespace Presentation.Controller;

public class AuthenticationController(IServiceManager serviceManager):ApiBaseController
{
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await serviceManager.AuthenticationService.LoginAsync(dto);
        return Ok(result);
    }
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto dto)
    {
        var result = await serviceManager.AuthenticationService.RegisterAsync(dto);
        return Ok(result);
    }

    [HttpPost("forgotPassword")]
    public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
    {
        await serviceManager.AuthenticationService.ForgotPasswordAsync(dto);
        return Ok(new {message = "Forgot password sent"});
    }
    [HttpPost("resetPassword")]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        await serviceManager.AuthenticationService.ResetPasswordAsync(dto);
        return Ok(new {message="Password reset successful"});
    }
}