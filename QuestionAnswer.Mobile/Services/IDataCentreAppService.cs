using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.DTO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionAnswer.Mobile.Model.QuestionItemModel;

namespace QuestionAnswer.Mobile.Services
{
    public interface IDataCentreAppService : IDataCentreService
    {
        public new Task<IList<MessageItem>> GetCommentsOfAnswer(Guid userId, Guid idAnswer, int countStart);

        public new Task<IList<AnswerItem>> GetAnswersToQuestion(Guid userId, Guid idQuestion, int countStart);

        public new Task<IList<QuestionItem>> GetRandomQuestions(Guid userId, int startCount);

        public new Task<IList<QuestionItem>> GetQuestionsFromUser(Guid userId, int startCount);
    }
}
