using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using QuestionAnswer.DTO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Api.Tests
{
    internal static class HttpClientTestAuth
    {
        public static HttpClient GetTestHttpClient(IDataBaseService dataCentreService)
        {
            return new WebApplicationFactory<Api.Program>().WithWebHostBuilder(bulder =>
            {
                bulder.ConfigureServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                    var service = services.SingleOrDefault(x => x.ServiceType == typeof(IDataBaseService));

                    services.Remove(service!);

                    services.AddTransient(x => dataCentreService);
                });

            }).CreateClient();
        }
    }
}
