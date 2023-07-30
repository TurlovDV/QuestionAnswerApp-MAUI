using Moq;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.DTO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Api.Tests
{
    public class QuestionsControllerTest
    {
        [Fact]
        public async void GetRandomQuestions_SendRequest_IsTestQuestionsEqualGetRandomQuestions()
        {
            //Arrange

            IList<QuestionItemDTO> questionItems = new List<QuestionItemDTO>()
            {
                new QuestionItemDTO()
                {
                    UserName = "1",
                    Id = Guid.NewGuid(),
                    Title = "11",
                    UserId = Guid.NewGuid()
                },
                new QuestionItemDTO()
                {
                    UserName = "2",
                    Id = Guid.NewGuid(),
                    Title = "22",
                    UserId = Guid.NewGuid()
                },
                new QuestionItemDTO()
                {
                    UserName = "3",
                    Id = Guid.NewGuid(),
                    Title = "33",
                    UserId = Guid.NewGuid()
                }
            };

            var userTest = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Rating = 0
            };

            var mock = new Mock<IDataBaseService>();

            mock.Setup(e => e.GetRandomQuestions(userTest.Id, 10, 0))
                .Returns(Task.Run(() => questionItems)!);

            var httpClient = HttpClientTestAuth.GetTestHttpClient(mock.Object);

            //Act

            var httpResponse = await httpClient.GetAsync($"questions?startCount=0&userId={userTest.Id}");
            var getRandomQuestions = await httpResponse.Content.ReadFromJsonAsync<List<QuestionItemDTO>>();

            //Assert

            Assert.Equal(questionItems, getRandomQuestions);
        }

        [Fact]
        public async void GetQuestionsFromUser_SendRequest_IsTestQuestionsFromUserEqualGetQuestionsFromAnswer()
        {
            //Arrange

            IList<QuestionItemDTO> questionItems = new List<QuestionItemDTO>()
            {
                new QuestionItemDTO()
                {
                    UserName = "1",
                    Id = Guid.NewGuid(),
                    Title = "11",
                    UserId = Guid.NewGuid()
                },
                new QuestionItemDTO()
                {
                    UserName = "2",
                    Id = Guid.NewGuid(),
                    Title = "22",
                    UserId = Guid.NewGuid()
                },
                new QuestionItemDTO()
                {
                    UserName = "3",
                    Id = Guid.NewGuid(),
                    Title = "33",
                    UserId = Guid.NewGuid()
                }
            };

            var userTest = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Rating = 0
            };

            var mock = new Mock<IDataBaseService>();

            mock.Setup(e => e.GetQuestionsFromUser(userTest.Id, 10, 0))
                .Returns(Task.Run(() => questionItems)!);

            var httpClient = HttpClientTestAuth.GetTestHttpClient(mock.Object);

            //Act

            var httpResponse = await httpClient.GetAsync($"questions/user?startCount=0&userId={userTest.Id}");
            var getRandomQuestions = await httpResponse.Content.ReadFromJsonAsync<List<QuestionItemDTO>>();

            //Assert

            Assert.Equal(questionItems, getRandomQuestions);
        }
    }
}
