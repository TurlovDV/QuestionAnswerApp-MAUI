using QuestionAnswer.DTO.Model.QuestionItemModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.DTO.Model
{
    public class User
    {
        public override bool Equals(object? obj)
        {
            User? user = obj as User;
            if (user is null)
                return false;

            return this.Id == user.Id;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string? ImagePath { get; set; }

        public string? Name { get; set; }

        public int Rating { get; set; }

        public int CountMyQuestions { get; set; }
        
        public int AnswersToQuestions { get; set; }

        public Guid Id { get; set; }
    }
}
