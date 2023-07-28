using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.DTO.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuestionAnswer.Api.Client
{
    public class DataCentreService : IDataCentreService
    {
        private HttpClient httpClientAuth;

        //public Guid UserId { get; set; }

        public DataCentreService()
        {
            httpClientAuth = new();
            httpClientAuth.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiConfiguration.AccessToken);
        }

        public async Task<HttpResponseMessage> GetHttpAndCheckExpinseToken(string requestUri)
        {
            var response = await httpClientAuth.GetAsync(requestUri);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await RefreshAccessToken();
                return await httpClientAuth.GetAsync(requestUri);
            }
            
            return response;
        }

        public async Task<HttpResponseMessage> PostAsJsonAndCheckToken<T>(string requestUri, T item)
        {
            var response = await httpClientAuth.PostAsJsonAsync<T>(requestUri, item);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await RefreshAccessToken();
                return await httpClientAuth.PostAsJsonAsync<T>(requestUri, item);
            }

            return response;
        }

        public async Task RefreshAccessToken()
        {
            var httpAuth = new HttpClient();
            httpAuth.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiConfiguration.RefreshToken);

            var uri = ApiConfiguration.BASE_ADRESS + $"api/refresh?userId={ApiConfiguration.UserId}";

            var response = await httpAuth.GetAsync(uri);

            var authResponse = await response.Content.ReadAsStringAsync();

            ApiConfiguration.AccessToken = authResponse;
            httpClientAuth = new();
            httpClientAuth.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResponse);

        }

        public async Task<IList<AnswerItemDTO>> GetAnswersToQuestion(Guid userId, Guid idQuestion, int countStart)
        {
            var uri = ApiConfiguration.BASE_ADRESS + $"questions/answers?questionId={idQuestion}&countStart={countStart}&userId={userId}";
            
            var httpResponse = await GetHttpAndCheckExpinseToken(uri);
                
            //await httpClientAuth.GetAsync(BASE_ADRESS + $"questions/answers?questionId={idQuestion}&countStart={countStart}&userId={userId}");
            
            
            var result = await httpResponse.Content.ReadFromJsonAsync<List<AnswerItemDTO>>();
            return result!;
        }

        public async Task<IList<MessageItemDTO>> GetCommentsOfAnswer(Guid userId, Guid idAnswer, int countStart)
        {
            var uri = ApiConfiguration.BASE_ADRESS + $"questions/answers/comments?answerId={idAnswer}&startCount={countStart}&userId={userId}";

            var httpResponse = await GetHttpAndCheckExpinseToken(uri); 
                //await httpClientAuth.GetAsync(BASE_ADRESS + $"questions/answers/comments?answerId={idAnswer}&startCount={countStart}&userId={userId}");
            
            
            var result = await httpResponse.Content.ReadFromJsonAsync<List<MessageItemDTO>>();
            return result!;
        }

        public Task<IList<Notification>> GetNotificaation()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<QuestionItemDTO>> GetRandomQuestions(Guid userId, int startCount)
        {
            var uri = ApiConfiguration.BASE_ADRESS + $"questions?startCount={startCount}&userId={userId}";


            var httpResponse = await GetHttpAndCheckExpinseToken(uri); 
                
                //await httpClientAuth.GetAsync(BASE_ADRESS + $"questions?startCount={startCount}&userId={userId}");

            var str = await httpResponse.Content.ReadAsStringAsync();

            var result = await httpResponse.Content.ReadFromJsonAsync<List<QuestionItemDTO>>();
            return result!;        
        }

        public async Task<IList<QuestionItemDTO>> GetQuestionsFromUser(Guid userId, int startCount)
        {
            var uri = ApiConfiguration.BASE_ADRESS + $"questions/user?startCount={startCount}&userId={userId}";

            var httpResponse = await GetHttpAndCheckExpinseToken(uri); 
                
                //await httpClientAuth.GetAsync(BASE_ADRESS + $"questions/user?startCount={startCount}&userId={userId}");

            var str = await httpResponse.Content.ReadAsStringAsync();

            var result = await httpResponse.Content.ReadFromJsonAsync<List<QuestionItemDTO>>();
            return result!;
        }

        public async Task<User?> GetUser(Guid id)
        {
            var uri = ApiConfiguration.BASE_ADRESS + $"users?userid={id}";
            var httpResponse = await GetHttpAndCheckExpinseToken(uri);
                //await httpClientAuth.GetAsync(BASE_ADRESS + $"users?userid={id}");
            var result = await httpResponse.Content.ReadFromJsonAsync<User>();
            return result;
        }

        public async Task SendNewAnswerToQuestion(CreateAnswerToQuestion createAnswer)
        {
            var uri = ApiConfiguration.BASE_ADRESS + "questions/answers";
            var response = await PostAsJsonAndCheckToken<CreateAnswerToQuestion>(uri, createAnswer);
                //await httpClientAuth.PostAsJsonAsync<CreateAnswerToQuestion>(BASE_ADRESS + "questions/answers", createAnswer);

            //Console.WriteLine("Запрос выполнен " + response.StatusCode.ToString());
        }

        public async Task SendNewCommentToAnswer(CreateCommentToAnswer createComment)
        {
            var uri = ApiConfiguration.BASE_ADRESS + "questions/answers/comments";
            var response = await PostAsJsonAndCheckToken<CreateCommentToAnswer>(uri, createComment);
                //await httpClientAuth.PostAsJsonAsync<CreateCommentToAnswer>(BASE_ADRESS + "questions/answers/comments", createComment);

            //Console.WriteLine("Запрос выполнен " + response.StatusCode.ToString());
        }

        public async Task AddNewUser(CreateNewUser createNewUser)
        {
            var uri = ApiConfiguration.BASE_ADRESS + "users";
            var response = await PostAsJsonAndCheckToken<CreateNewUser>(uri, createNewUser); 
                //await httpClientAuth.PostAsJsonAsync<CreateNewUser>(BASE_ADRESS + "users", createNewUser);

            //Console.WriteLine("Запрос выполнен " + response.StatusCode.ToString());
        }

        public async Task CreateNewQuestion(CreateQuestionItem questionItem)
        {
            var uri = ApiConfiguration.BASE_ADRESS + "questions";
            var response = await PostAsJsonAndCheckToken<CreateQuestionItem>(uri, questionItem);
                //await httpClientAuth.PostAsJsonAsync<CreateQuestionItem>(BASE_ADRESS + "questions", questionItem);

            //Console.WriteLine("Запрос выполнен " + response.StatusCode.ToString());
        }

        public async Task MessageLike(LikeItem likeItem)
        {
            var uri = ApiConfiguration.BASE_ADRESS + "likes";
            await PostAsJsonAndCheckToken<LikeItem>(uri, likeItem);
                //httpClientAuth.PostAsJsonAsync<LikeItem>(BASE_ADRESS + "likes", likeItem);
        }

        public async Task MessageDizLike(LikeItem likeItem)
        {
            var uri = ApiConfiguration.BASE_ADRESS + "dizLikes";
            await PostAsJsonAndCheckToken<LikeItem>(uri, likeItem);
                //httpClientAuth.PostAsJsonAsync<LikeItem>(BASE_ADRESS + "dizLikes", likeItem);
        }

        public async Task MessageCancelLike(LikeItem likeItem)
        {            
            var jsonContent = JsonContent.Create(likeItem);

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                Content = jsonContent,
                RequestUri = new Uri(ApiConfiguration.BASE_ADRESS + "likes")
            };

            var response = await httpClientAuth.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await RefreshAccessToken();
                await httpClientAuth.SendAsync(request);
            }
        }

        public async Task MessageCancelDizLike(LikeItem likeItem)
        {            
            var jsonContent = JsonContent.Create(likeItem);

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                Content = jsonContent,
                RequestUri = new Uri(ApiConfiguration.BASE_ADRESS + "dizLikes")
            };

            var response = await httpClientAuth.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await RefreshAccessToken();
                await httpClientAuth.SendAsync(request);
            }
        }

        public async Task DeleteQuestion(Guid questionId)
        {            
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(ApiConfiguration.BASE_ADRESS + $"questions?questionId={questionId}")
            };

            var response = await httpClientAuth.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await RefreshAccessToken();
                await httpClientAuth.SendAsync(request);
            }
        }

        public async Task AddViewQuestion(Guid questionId)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiConfiguration.BASE_ADRESS + $"questions/views?questionId={questionId}&count=1")
            };

            var response = await httpClientAuth.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await RefreshAccessToken();
                await httpClientAuth.SendAsync(request);
            }
        }
    }
}
