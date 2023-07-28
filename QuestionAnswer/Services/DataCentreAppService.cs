using AutoMapper;
using Microsoft.Maui.Graphics.Text;
using QuestionAnswer.Api.Client;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.DTO.Services;
using QuestionAnswer.Mobile.Model.QuestionItemModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Services
{
    class DataCentreAppService : DataCentreService, IDataCentreAppService
    {
        private IStorageOptionsService storageOptionsService;
        private static IMapper Mapper; 
        static DataCentreAppService()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AnswerItem, AnswerItemDTO>().ReverseMap();
                cfg.CreateMap<QuestionItem, QuestionItemDTO>().ReverseMap();
                cfg.CreateMap<MessageItem, MessageItemDTO>().ReverseMap();
            });

            Mapper = config.CreateMapper();            
        }

        public DataCentreAppService()
        {
            storageOptionsService = ServiceProvider.GetService<IStorageOptionsService>();
            //InitializationUserService.RefreshToken = storageOptionsService.GetRefreshToken();
            //this.UserId = storageOptionsService.GetUserId();
        }

        async Task<IList<AnswerItem>> IDataCentreAppService.GetAnswersToQuestion(Guid userId, Guid idQuestion, int countStart)
        {
            var result = new List<AnswerItem>();

            var answerItemsDTO = await base.GetAnswersToQuestion(userId, idQuestion, countStart);

            answerItemsDTO.ToList().ForEach(x =>
            {
                result.Add(Mapper.Map<AnswerItemDTO, AnswerItem>(x));
            });

            return result;
        }

        async Task<IList<MessageItem>> IDataCentreAppService.GetCommentsOfAnswer(Guid userId, Guid idAnswer, int countStart)
        {
            var result = new List<MessageItem>();

            var commentsItemsDTO = await base.GetCommentsOfAnswer(userId, idAnswer, countStart);

            commentsItemsDTO.ToList().ForEach(x =>
            {
                result.Add(Mapper.Map<MessageItemDTO, MessageItem>(x));
            });

            return result;
        }

        async Task<IList<QuestionItem>> IDataCentreAppService.GetQuestionsFromUser(Guid userId, int startCount)
        {
            var result = new List<QuestionItem>();

            var questionsItemsDTO = await base.GetQuestionsFromUser(userId, startCount);

            questionsItemsDTO.ToList().ForEach(x =>
            {
                result.Add(Mapper.Map<QuestionItemDTO, QuestionItem>(x));
            });

            return result;
        }

        async Task<IList<QuestionItem>> IDataCentreAppService.GetRandomQuestions(Guid userId, int startCount)
        {
            var result = new List<QuestionItem>();

            var questionsItemsDTO = await base.GetRandomQuestions(userId, startCount);

            questionsItemsDTO.ToList().ForEach(x =>
            {
                result.Add(Mapper.Map<QuestionItemDTO, QuestionItem>(x));
            });

            return result;
        }

        public static Dictionary<string, string> GetClaims(string token)
        {
            var TokenInfo = new Dictionary<string, string>();

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claims = jwtSecurityToken.Claims.ToList();

            foreach (var claim in claims)
            {
                TokenInfo.Add(claim.Type.ToString().Split('/').Last(), claim.Value);
            }

            return TokenInfo;
        }
    }
}
