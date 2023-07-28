using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.DTO.Model
{
    public class CreateQuestionItem
    {
        public Guid UserId { get; set; }

        public Guid Id { get; set; } 

        public string? Title { get; set; }

        public string? Description { get; set; }

        public List<MediaItem>? MediaItems { get; set; }
    }
}
