using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.DTO.Model
{
    public class CreateAnswerToQuestion
    {
        public Guid QuestionId { get; set; }

        public string? Description { get; set; }

        public Guid UserId { get; set; }

        public Guid Id { get; set; }
    }
}
