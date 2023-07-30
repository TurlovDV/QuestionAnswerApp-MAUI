using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Newtonsoft.Json;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Api.Tests
{
    public class UsersControllerTests
    {           
        [Fact]
        public async void AddUser_SendRequest_IsOk()
        {            
            //Act

            var createNewUser = new CreateNewUser()
            {
                AuthUserItem = new AuthUserItem()
                {
                    Email = "Hello",
                    Password = "24t54323"                    
                },
                Id = Guid.NewGuid(),
                UserName = "Daniil"
            };

            var mock = new Mock<IDataBaseService>();

            mock.Setup(e => e.AddNewUser(default!)).Returns(Task.CompletedTask);

            var httpClient = HttpClientTestAuth.GetTestHttpClient(mock.Object);

            //Arrange

            var response = await httpClient.PostAsync("users", JsonContent.Create(createNewUser));

            //Assert

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetUser_SendRequest_IsTestUserEqualGetUser()
        {
            //Arrange

            var createNewTestUser = new CreateNewUser()
            {
                AuthUserItem = new AuthUserItem()
                {
                    Email = "Hello",
                    Password = "24t54323"
                },
                Id = Guid.NewGuid(),
                UserName = "Daniil"
            };

            var userTest = new User()
            {
                Id = createNewTestUser.Id,
                Name = createNewTestUser.UserName,
                Rating = 0
            };

            var mock = new Mock<IDataBaseService>();

            mock.Setup(e => e.GetUser(createNewTestUser.Id))
                .Returns(Task.Run(() => userTest)!);

            var httpClient = HttpClientTestAuth.GetTestHttpClient(mock.Object);

            //Act

            var httpResponse = await httpClient.GetAsync($"users?userId={createNewTestUser.Id}");
            var userGet = await httpResponse.Content.ReadFromJsonAsync<User>();

            ////Чтобы не переопределять Equals, GetHash вполне уместно сериализовать обьекты в json и сравнить
            //var userGetJson = Newtonsoft.Json.JsonConvert.SerializeObject(userGet);
            //var userTestJson = Newtonsoft.Json.JsonConvert.SerializeObject(userTest);

            //Assert

            Assert.Equal(userTest, userGet);
        }
    }
}
