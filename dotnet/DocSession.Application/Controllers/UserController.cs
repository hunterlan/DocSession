using System.Security.Claims;
using System.Text;
using DocSession.Application.Models.Users.UserCreate;
using DocSession.Application.Models.Users.UserLogin;
using DocSession.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace DocSession.Application.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly ApplicationContext _context;
    private readonly IValidator<UserLoginModel> _validator;
    private readonly IConfiguration config;

    public UserController(ApplicationContext context, IValidator<UserLoginModel> validator, IConfiguration configuration)
    {
      _context = context;
      _validator = validator;
      config = configuration;
    }

    [HttpPost]
    public async Task<IResult> UserLogin(UserLoginModel userLogin)
    {
      var validationResult = await _validator.ValidateAsync(userLogin);

      if (!validationResult.IsValid)
      {
        return Results.ValidationProblem(validationResult.ToDictionary());
      }

      //TODO: Don't store password as plain text

      var user = _context.Users.Include(user => user.Person)
        .ThenInclude(person => person.Doctor)
        .Include(user => user.Person).ThenInclude(person => person.Admin)
        .FirstOrDefault(u => u.Person.Email == userLogin.Username && u.Password == userLogin.Password);

      if (user is null)
      {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
          {"loginError", new[] {"Login or password are incorrect!"}}
        });
      }

      string roleName;

      if (user.Person.Doctor is not null)
      {
        roleName = "Doctor";
      }
      else if (user.Person.Admin is not null)
      {
        roleName = "Admin";
      }
      else
      {
        roleName = "User";
      }

      var key = Encoding.ASCII.GetBytes(config["Jwt:Key"]!);
      var handler = new JsonWebTokenHandler();

      var claims = new List<Claim>
      {
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new(JwtRegisteredClaimNames.Email, user.Person.Email),
        new(ClaimTypes.Role, roleName),
        new(ClaimTypes.NameIdentifier, user.PersonId.ToString())
      };

      var token = handler.CreateToken(new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddHours(int.Parse(config["Jwt:TokenLifetime"]!)),
        Audience = config["Jwt:Audience"],
        Issuer = config["Jwt:Issuer"],
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      });

      return Results.Ok(token);
    }

    public async Task<IResult> CreateUser(UserCreateModel createUser)
    {
      var newUser = new User(createUser);
      await _context.Users.AddAsync(newUser);
      await _context.SaveChangesAsync();

      return Results.Created();
    }
  }
}
