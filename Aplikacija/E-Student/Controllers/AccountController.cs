using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
public class AccountController : ControllerBase
{
    private readonly IConfiguration Configuration;

    public AccountController(IConfiguration configuration)
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

            LoginRegister account = mapper.FirstOrDefault<LoginRegister>("WHERE email=?", model.Email);
            var hash = HashPassword(model.Password!, account.Salt!, 10101, 70);
            if (account == null)
            {
                return Unauthorized();
            }
            if (account.Password_Hash != hash)
            {
                return BadRequest("Password doesn't match");
            }
            var authClaims = new List<Claim>
                            {
                               new Claim(ClaimTypes.Name,account.Email!),
                               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                               new Claim(ClaimTypes.Role, account.Role!)
                            };
            SymmetricSecurityKey authSiginKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:Secret"]));
            JwtSecurityToken token = new JwtSecurityToken(
            issuer: Configuration["JWT:ValidIssuer"],
            audience: Configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSiginKey, SecurityAlgorithms.HmacSha256Signature)
            );
            var student = localSession.Execute("SELECT odobren FROM student WHERE email='" + model.Email + "'");
            cluster.Shutdown();
            bool test = false;
            foreach (var i in student)
            {
                test = i.GetValue<bool>("odobren");
                break;
            }
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo.ToString("yyyy-MM-ddThh:mm:ss"),
                role = account.Role,
                odobren = test

            });
        }
        return Unauthorized();
    }
    [HttpPost]
    [AllowAnonymous]
    [Route("registerStudent")]
    public object Registration(RegistrationModel model)
    {
        if (ModelState.IsValid)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

            var student = new Student()
            {
                Email = model.Email,
                GodinaUpisa = model.GodinaUpisa,
                Ime = model.Ime,
                Indeks = model.Indeks,
                Odobren = false,
                Prezime = model.Prezime,
                Semestar = model.Semestar,
                Smer = model.Smer,
                Dugovanje = "0"
            };
            var salt = GenerateSalt(70);
            var hashPass = HashPassword(model.Password!, salt, 10101, 70);
            var register = new LoginRegister()
            {
                Email = model.Email,
                Role = "Student",
                Password_Hash = hashPass,
                Salt = salt
            };

            try
            {
                Cassandra.ISession localSession = cluster.Connect("test");
                IMapper mapper = new Mapper(localSession);
                var check = mapper.FirstOrDefault<LoginRegister>("WHERE email=?", register.Email);

                if (check != null)
                {
                    return BadRequest("Postoji osoba sa tim emailom");
                }
                mapper.InsertIfNotExists<Student>(student);
                mapper.InsertIfNotExists<LoginRegister>(register);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            finally
            {
                cluster.Shutdown();
            }
        }
        else
        {
            return BadRequest();
        }
    }
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [AllowAnonymous]
    [Route("registerProfesor")]
    public object ProfesorRegistration(ProfesorRegistrationModel model)
    {
        if (ModelState.IsValid)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

            var profesor = new Profesor()
            {
                Email = model.Email,
                Ime = model.Ime,
                Prezime = model.Prezime,
                Br_telefona = model.Br_telefona,
                Kancelarija = model.Kancelarija
            };
            var salt = GenerateSalt(70);
            var hashPass = HashPassword(model.Password!, salt, 10101, 70);
            var register = new LoginRegister()
            {
                Email = model.Email,
                Role = "Profesor",
                Password_Hash = hashPass,
                Salt = salt
            };

            try
            {
                Cassandra.ISession localSession = cluster.Connect("test");
                IMapper mapper = new Mapper(localSession);
                var check = mapper.FirstOrDefault<LoginRegister>("WHERE email=?", register.Email);

                if (check != null)
                {
                    return BadRequest("Postoji osoba sa tim emailom");
                }
                mapper.InsertIfNotExists<Profesor>(profesor);
                mapper.InsertIfNotExists<LoginRegister>(register);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cluster.Shutdown();
            }
        }
        else
        {
            return BadRequest();
        }
    }
    public static string GenerateSalt(int nSalt)
    {
        var saltBytes = new byte[nSalt];

        using (var provider = new RNGCryptoServiceProvider())
        {
            provider.GetNonZeroBytes(saltBytes);
        }

        return Convert.ToBase64String(saltBytes);
    }

    public static string HashPassword(string password, string salt, int nIterations, int nHash)
    {
        var saltBytes = Convert.FromBase64String(salt);

        using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations))
        {
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
        }
    }


}




