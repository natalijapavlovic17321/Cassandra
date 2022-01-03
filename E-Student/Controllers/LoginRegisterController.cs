using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cassandra;
using Cassandra.Mapping;
using E_Student.Models;
using E_Student.Models.LoginRegisters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace E_Student.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginRegister : ControllerBase
{
    private readonly IConfiguration Configuration;

    public LoginRegister(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("Login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        if (ModelState.IsValid)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            Cassandra.ISession localSession = cluster.Connect("test");
            //var localSession = SessionManager.GetSession();
            IMapper mapper = new Mapper(localSession);

            LoginRegisterModels student = mapper.FirstOrDefault<LoginRegisterModels>("WHERE email=? AND password_hash=?  ALLOW FILTERING", model.UserName, model.Password);
            cluster.Shutdown();
            if (student == null)
            {
                return Unauthorized();
            }
            var authClaims = new List<Claim>
                            {
                               new Claim(ClaimTypes.Name,student.Email!),
                               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                               new Claim(ClaimTypes.Role, student.Role!)
                            };
            SymmetricSecurityKey authSiginKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:Secret"]));
            JwtSecurityToken token = new JwtSecurityToken(
            issuer: Configuration["JWT:ValidIssuer"],
            audience: Configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSiginKey, SecurityAlgorithms.HmacSha256Signature)
            );
            return Ok(new
            {
                tokens = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo.ToString("yyyy-MM-ddThh:mm:ss"),
                role = student.Role
            });
        }
        return Unauthorized();
    }
}



