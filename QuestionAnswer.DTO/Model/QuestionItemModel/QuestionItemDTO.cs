using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.DTO.Model.QuestionItemModel
{
    public class QuestionItemDTO : MessageItemDTO
    {
        public int Views { get; set; }

        public string? Title { get; set; }  

        public int CountAnswers { get; set; }
    }
}
