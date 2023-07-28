using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.DTO.Services;

namespace QuestionAnswer.Api.Controllers
{
    [Authorize(Roles = "User")]
    [Route("questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        IDataBaseService dataBaseService;
        public QuestionsController(IDataBaseService dataBaseService)
        {
            this.dataBaseService = dataBaseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<QuestionItemDTO>>> GetRandomQuestions(Guid userId, int startCount)
        {
            IList<QuestionItemDTO> questions;
            
            //if(userId == Guid.Empty)
            questions = await dataBaseService.GetRandomQuestions(userId, 10, startCount);
            //else
            //    questions = await dataBaseService.GetQuestionsFromUser(userId, 10, startCount);

            return Ok(questions);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteQuestion(Guid questionId)
        {
            await dataBaseService.DeleteQuestion(questionId);
            
            return Ok();
        }


        [HttpGet]
        [Route("user")]
        public async Task<ActionResult<List<QuestionItemDTO>>> GetQuestionsFromUser(Guid userId, int startCount)
        {
            IList<QuestionItemDTO> questions;

            questions = await dataBaseService.GetQuestionsFromUser(userId, 10, startCount);

            return Ok(questions);
        }

        [HttpPost]
        public async Task<ActionResult> AddQuestion(CreateQuestionItem questionItem)
        {
            try
            {
                await dataBaseService.CreateNewQuestion(questionItem);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("views")]
        public async Task<ActionResult> AddViews(Guid questionId, int count)
        {
            try
            {
                await dataBaseService.AddViewQuestion(questionId, count);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
