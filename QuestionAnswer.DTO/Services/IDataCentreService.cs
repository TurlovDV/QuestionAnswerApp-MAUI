using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Model.QuestionItemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.DTO.Services
{
    public interface IDataCentreService 
    {
        //public Guid UserId { get; set; }

        public Task AddViewQuestion(Guid questionId);
        public Task<IList<MessageItemDTO>> GetCommentsOfAnswer(Guid userId, Guid idAnswer, int countStart);

        public Task SendNewCommentToAnswer(CreateCommentToAnswer createComment);

        public Task<IList<AnswerItemDTO>> GetAnswersToQuestion(Guid userId, Guid idQuestion, int countStart);
        
        public Task<User?> GetUser(Guid id);
        
        public Task<IList<Notification>> GetNotificaation();

        public Task<IList<QuestionItemDTO>> GetRandomQuestions(Guid userId, int startCount);

        public Task<IList<QuestionItemDTO>> GetQuestionsFromUser(Guid userId, int startCount);

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
