using QuestionAnswer.DTO.Services;

namespace QuestionAnswer.Api.Client.Test
{
    [TestClass]
    public class DataCentreServiceTest
    {
        IDataCentreService service = new DataCentreService();
        Guid userId;

        [ClassInitialize]
        public void Initialization()
        {
            userId = Guid.NewGuid();
            service.AddNewUser(new DTO.Model.CreateNewUser()
            {
                UserName = "Test",
                Id = userId
            });
        }

        [TestMethod]
        public async void GetUser_Test()
        {
            var user = await service.GetUser(userId);

            Assert.AreEqual(user!.Id, userId);
            Assert.AreEqual(user!.Name, "Test");
        }
    }
}