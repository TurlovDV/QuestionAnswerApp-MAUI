using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Services;

namespace QuestionAnswer.Api.Controllers
{
    [Authorize(Roles = "User")]
    [Route("dizLikes")]
    [ApiController]
    public class DizLikesController : ControllerBase
    {
        IDataBaseService dataBaseService;
        public DizLikesController(IDataBaseService dataBaseService)
        {
            this.dataBaseService = dataBaseService;
        }

        [HttpPost]
        public async Task<ActionResult> DizLike(LikeItem likeItem)
        {
            await dataBaseService.MessageDizLike(likeItem);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> CancelDizLike(LikeItem likeItem)
        {
            await dataBaseService.MessageCancelDizLike(likeItem);

            return Ok();
        }
    }
}
