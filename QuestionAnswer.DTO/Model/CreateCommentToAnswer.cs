﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.DTO.Model
{
    public class CreateCommentToAnswer
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid AnswerId { get; set; } 

        public string? Description { get; set; }
    }
}