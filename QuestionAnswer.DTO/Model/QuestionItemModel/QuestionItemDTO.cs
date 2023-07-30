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
        public override bool Equals(object? obj)
        {
            QuestionItemDTO? question = obj as QuestionItemDTO;
            if (question is null)
                return false;

            return this.Id == question.Id;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int Views { get; set; }

        public string? Title { get; set; }  

        public int CountAnswers { get; set; }
    }
}
