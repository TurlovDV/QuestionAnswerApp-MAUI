using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Mobile.Model.QuestionItemModel
{
    public class QuestionItem : MessageItem
    {
        public int Views { get; set; }

        public string Title { get; set; }  

        public int CountAnswers { get; set; }

        public ObservableCollection<AnswerItem> Answers { get; set; }
    }
}
