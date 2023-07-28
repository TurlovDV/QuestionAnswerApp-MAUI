using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Api.Client
{
    public static class ApiConfiguration
    {
        public const string BASE_ADRESS = "http://localhost:3000/";

        
        public static string RefreshToken = string.Empty;

        
        public static string AccessToken = string.Empty;

        public static Guid UserId = Guid.Empty;
    }
}
