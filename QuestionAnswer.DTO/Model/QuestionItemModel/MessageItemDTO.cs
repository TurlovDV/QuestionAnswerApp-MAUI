using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.DTO.Model.QuestionItemModel
{
    public class MessageItemDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string? Description { get; set; }

        public string? UserName { get; set; }
        
        public int Rating { get; set; }

        public int CountLike { get; set; }

        public int CountDizLike { get; set; }

        public bool IsLike { get; set; }

        public bool IsDizLike { get; set; }
    }
}
