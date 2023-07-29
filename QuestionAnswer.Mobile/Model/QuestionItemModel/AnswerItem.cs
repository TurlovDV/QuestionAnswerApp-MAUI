using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Mobile.Model.QuestionItemModel
{
    public partial class AnswerItem : MessageItem
    {
        public ObservableCollection<MessageItem> Comments { get; set; }

        [ObservableProperty]
        public bool isVisibleShowMore = true;
        
        public int CountComments { get; set; }

    }
}
