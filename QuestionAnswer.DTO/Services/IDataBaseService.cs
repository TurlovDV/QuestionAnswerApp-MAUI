using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.DTO.Services
{
    public interface IDataBaseService
    {
        public Task<IList<MessageItemDTO>> GetCommentsOfAnswer(Guid userId, Guid idAnswer, int count, int countStart);

        public Task SendNewCommentToAnswer(CreateCommentToAnswer createComment);

        public Task<IList<AnswerItemDTO>> GetAnswersToQuestion(Guid userId, Guid idQuestion, int count, int countStart);

        public Task<User?> GetUser(Guid id);

        public Task<Guid> Authentication(string password, string login);

        public Task<IList<Notification>> GetNotificaation();

        public Task<IList<QuestionItemDTO>> GetRandomQuestions(Guid userId, int count, int startCount);

        public Task<IList<QuestionItemDTO>> GetQuestionsFromUser(Guid userId, int count, int startCount);

        public Task CreateNewQuestion(CreateQuestionItem questionItem);

        public Task SendNewAnswerToQuestion(CreateAnswerToQuestion createAnswer);

        public Task AddNewUser(CreateNewUser createNewUser);

        public Task MessageLike(LikeItem likeItem);

        public Task MessageDizLike(LikeItem likeItem);

        public Task MessageCancelLike(LikeItem likeItem);

        public Task MessageCancelDizLike(LikeItem likeItem);

        public Task DeleteQuestion(Guid questionId);
    }
}
