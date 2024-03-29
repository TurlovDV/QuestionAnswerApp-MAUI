﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace QuestionAnswer.Api
{
    public class AuthOptions
    {
        public const string ISSUER = "QuestionAnswerApi"; // издатель токена
        public const string AUDIENCE = "QuestionAnswerClient"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
