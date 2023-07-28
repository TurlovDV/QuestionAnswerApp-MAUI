using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.DTO.Services;

namespace QuestionAnswer.Api.Controllers
{
    [Authorize(Roles = "User")]
    [Route("questions/answers")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        IDataBaseService dataBaseService;
        public AnswersController(IDataBaseService dataBaseService)
        {
            this.dataBaseService = dataBaseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AnswerItemDTO>>> GetAnswers(Guid userId, Guid questionId, int countStart)
        {
            var result = await dataBaseService.GetAnswersToQuestion(userId, questionId, 10, countStart);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddAnswer(CreateAnswerToQuestion answer)
        {
            try
            {
                await dataBaseService.SendNewAnswerToQuestion(answer);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
