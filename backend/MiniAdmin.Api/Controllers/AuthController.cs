using Microsoft.AspNetCore.Mvc;
using MiniAdmin.Api.Auth;

namespace MiniAdmin.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _tokens;

    public AuthController(JwtTokenService tokens)
    {
        _tokens = tokens;
    }

    [HttpPost("login")]
    public ActionResult<LoginResponse> Login(LoginRequest request)
    {
        if (request.Username == "admin" && request.Password == "secret")
        {
            return Ok(new LoginResponse { Token = _tokens.CreateToken("admin", "Admin") });
        }
        else if (request.Username == "user" && request.Password == "secret")
        {
            return Ok(new LoginResponse { Token = _tokens.CreateToken("user", "User") });
        }
        return Unauthorized();
    }
}
