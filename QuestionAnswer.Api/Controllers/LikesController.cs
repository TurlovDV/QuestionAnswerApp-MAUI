using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.DTO.Services;

namespace QuestionAnswer.Api.Controllers
{
    [Authorize(Roles = "User")]
    [Route("likes")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        IDataBaseService dataBaseService;
        public LikesController(IDataBaseService dataBaseService)
        {
            this.dataBaseService = dataBaseService;
        }

        [HttpPost]
        public async Task<ActionResult> Like(LikeItem likeItem)
        {
            await dataBaseService.MessageLike(likeItem);
            
            return Ok();
        }
        
        [HttpDelete]
        public async Task<ActionResult> CancelLike(LikeItem likeItem)
        {
            await dataBaseService.MessageCancelLike(likeItem);

            return Ok();
        }
    }
}
