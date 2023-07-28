using QuestionAnswer.DTO.Model.QuestionItemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.DTO.Model
{
    public class User
    {
        public string? ImagePath { get; set; }

        public string? Name { get; set; }

        public int Rating { get; set; }

        public int CountMyQuestions { get; set; }

        //public List<QuestionItem>? MyQuestions { get; set; }

        public int AnswersToQuestions { get; set; }

        public Guid Id { get; set; }
    }
}
