using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Services;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace QuestionAnswer.Api.Controllers
{
    [Authorize(Roles = "User")]
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IDataBaseService dataBaseService;
        public UsersController(IDataBaseService dataBaseService)
        {
            this.dataBaseService = dataBaseService;
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(CreateNewUser user)
        {
            try
            {
                await dataBaseService.AddNewUser(user);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }               
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUser(Guid userid)
        {
            var result = await dataBaseService.GetUser(userid);

            if (result is not null)
                return Ok(result);

            return NotFound();
        }
    }
}
