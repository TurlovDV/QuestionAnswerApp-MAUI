using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QuestionAnswer.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        IDataBaseService dataBaseService;
        public AuthController(IDataBaseService dataBaseService)
        {
            this.dataBaseService = dataBaseService;
        }

        [Route("authentication")]
        [HttpPost]
        public async Task<ActionResult<AuthResponse>> AuthenticationUser(AuthUserItem userItem)
        {
            Guid userId = await dataBaseService.Authentication(userItem.Password!, userItem.Email!);

            if(userId == Guid.Empty)
                return NotFound();
                        
            return Ok(new AuthResponse()
            {
                RefreshToken = CreateRefreshToken(userId),
                AccessToken = CreateAccessToken(userId)
            });
        }

        [Route("registration")]
        [HttpPost]
        public async Task<ActionResult<AuthResponse>> RegistrationUser(CreateNewUser createNewUser)
        {
            await dataBaseService.AddNewUser(createNewUser);

            return Ok(new AuthResponse()
            {
                AccessToken = CreateAccessToken(createNewUser.Id),
                RefreshToken = CreateRefreshToken(createNewUser.Id)
            });
        }

        [Authorize(Roles = "Refresh")]
        [Route("refresh")]
        [HttpGet]
        public ActionResult<string> Refresh(Guid userId) =>        
            CreateAccessToken(userId);
        
        string CreateAccessToken(Guid userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userId.ToString()),
                new Claim(ClaimTypes.Role, "User")
            };

            return new JwtSecurityTokenHandler().WriteToken(GetJwtSecurityToken(claims, DateTime.Now.AddSeconds(15)));
        }

        string CreateRefreshToken(Guid userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userId.ToString()),
                new Claim(ClaimTypes.Role, "Refresh")
            };

            return new JwtSecurityTokenHandler().WriteToken(GetJwtSecurityToken(claims, DateTime.Now.AddDays(7)));
        }

        JwtSecurityToken GetJwtSecurityToken(List<Claim> claims, DateTime expires) =>        
            new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: expires,
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));        
    }
}