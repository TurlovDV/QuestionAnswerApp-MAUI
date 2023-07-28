using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.DTO.Model
{
    public class MediaItem
    {
        public byte[]? ImageBytes { get; set; }

        public MediaType Type { get; set; }
    }

    public enum MediaType
    {
        Image
    };
}
