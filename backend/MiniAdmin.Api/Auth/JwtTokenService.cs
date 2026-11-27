using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MiniAdmin.Api.Auth;

// 老師提供：負責把 username／role 簽成 JWT 字串（第 17 章後面會用來驗票）
public class JwtTokenService
{
    public const string Issuer = "mini-admin";
    public const string Audience = "mini-admin-api";
    // 學習用固定金鑰（正式環境改讀設定／環境變數）
    public const string SigningKey = "mini-admin-dev-signing-key-32chars!!";

    public string CreateToken(string username, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
