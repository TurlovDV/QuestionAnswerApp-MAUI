using Microsoft.AspNetCore.Mvc;
using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Services;
using Microsoft.AspNetCore.Authorization;

namespace QuestionAnswer.Api.Controllers
{
    [Authorize(Roles = "User")]
    [Route("questions/answers/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        IDataBaseService dataBaseService;
        public CommentsController(IDataBaseService dataBaseService)
        {
            this.dataBaseService = dataBaseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MessageItemDTO>>> GetComments(Guid userId, Guid answerId, int startCount)
        {
            var comments = await dataBaseService.GetCommentsOfAnswer(userId, answerId, 2, startCount);

            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult> AddComments(CreateCommentToAnswer answer)
        {
            try
            {
                await dataBaseService.SendNewCommentToAnswer(answer);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
