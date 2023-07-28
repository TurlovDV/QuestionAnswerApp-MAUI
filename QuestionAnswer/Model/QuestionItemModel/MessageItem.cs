using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Mobile.Model.QuestionItemModel
{
    public partial class MessageItem : ObservableObject
    {        
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Description { get; set; }

        public string UserName { get; set; }

        public int Rating { get; set; }

        [ObservableProperty]
        public int countLike;

        [ObservableProperty]
        public int countDizLike;

        [ObservableProperty]
        public bool isLike;

        [ObservableProperty]
        public bool isDizLike;

        public bool IsMy { get; set; }        
    }
}
