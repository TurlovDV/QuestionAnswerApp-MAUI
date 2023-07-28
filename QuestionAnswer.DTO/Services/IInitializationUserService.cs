using QuestionAnswer.DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.DTO
{
    public interface IInitializationUserService
    {
        //public static string? RefreshToken { get; set; }

        //public static string? AccessToken { get; set; }

        public Task<AuthResponse?> RegistrationUser(CreateNewUser createNewUser);

        public Task<AuthResponse?> AuthenticationUser(AuthUserItem authUserItem);

    }
}
