using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos;

namespace Presentation.Controller;

public class AuthenticationController(IServiceManager serviceManager) : ApiBaseController
{
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await serviceManager.AuthenticationService.LoginAsync(dto);
        Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions()
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });
        return Ok(new { result.DisplayName, result.Email, result.AccessToken });
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
        return Ok(new { message = "Forgot password sent" });
    }

    [HttpPost("resetPassword")]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        await serviceManager.AuthenticationService.ResetPasswordAsync(dto);
        return Ok(new { message = "Password reset successful" });
    }

    [HttpPost("refreshToken")]
    public async Task<ActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken)) return Unauthorized("Missing refresh token");
        var res = await serviceManager.AuthenticationService.RefreshTokenAsync(refreshToken);
        Response.Cookies.Append("refreshToken", res.RefreshToken, new CookieOptions()
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });
        return Ok(new { res.DisplayName, res.Email, res.AccessToken });
    }

    [HttpPost("revokeRefreshToken")]
    public async Task<ActionResult> RevokeRefreshToken([FromBody] string refreshToken)
    {
        await serviceManager.AuthenticationService.RevokeRefreshTokenAsync(refreshToken);
        return Ok(new { message = "Refresh token revoked" });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("refreshToken");
        return Ok(new { message = "Logout successful" });
    }
}