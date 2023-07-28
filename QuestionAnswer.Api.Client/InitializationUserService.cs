using QuestionAnswer.Api.Client;
using QuestionAnswer.DTO;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Api.Client
{
    public class InitializationUserService : IInitializationUserService
    {
        public async Task<AuthResponse?> AuthenticationUser(AuthUserItem authUserItem)
        {
            HttpClient httpClient = new HttpClient();

            var jsonContent = JsonContent.Create(authUserItem);

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                Content = jsonContent,
                RequestUri = new Uri(ApiConfiguration.BASE_ADRESS + "api/authentication")
            };

            var response = await httpClient.SendAsync(httpRequestMessage);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();

            return new AuthResponse()
            {
                AccessToken = authResponse!.AccessToken,
                RefreshToken = authResponse!.RefreshToken
            };
        }

        public async Task<AuthResponse?> RegistrationUser(CreateNewUser createNewUser)
        {
            HttpClient httpClient = new HttpClient();

            var jsonContent = JsonContent.Create(createNewUser);

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                Content = jsonContent,
                RequestUri = new Uri(ApiConfiguration.BASE_ADRESS + "api/registration")
            };

            var response = await httpClient.SendAsync(httpRequestMessage);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();

            return new AuthResponse()
            {
                AccessToken = authResponse!.AccessToken,
                RefreshToken = authResponse!.RefreshToken
            };
        }
    }
}
