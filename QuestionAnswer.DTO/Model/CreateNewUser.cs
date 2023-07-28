using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.DTO.Model
{
    public class CreateNewUser
    {
        public AuthUserItem? AuthUserItem { get; set; }

        public string? UserName { get; set; }  

        public Guid Id { get; set; }
    }
}
