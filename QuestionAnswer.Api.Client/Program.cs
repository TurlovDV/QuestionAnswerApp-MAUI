using QuestionAnswer.DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Api.Client
{
    public class Program
    {
        public static async Task Main(string[] args) 
        {
            //DataCentreService dataCentreService = new DataCentreService();
            //var k = await dataCentreService.GetRandomQuestions(dataCentreService.UserId, 0);

            //k.ToList().ForEach(x => Console.WriteLine(x.Id));

            //await dataCentreService.CreateNewQuestion(new CreateQuestionItem()
            //{
            //    Description = "Hello",
            //    Id = Guid.NewGuid(),
            //    Title = "Hi",
            //    UserId = Guid.Parse("00fce14e-9498-4de7-8b76-f9d3f07fd5b2")
            //});

            //var r = await dataCentreService.GetCommentsOfAnswer(dataCentreService.UserId, Guid.Parse("00533878-27e2-4e77-b4dc-4e23bb8e43de"), 0);
            //r.ToList().ForEach(x => Console.WriteLine(x.Id));
        }
    }
}
