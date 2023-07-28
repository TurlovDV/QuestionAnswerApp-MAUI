using QuestionAnswer.Mobile.Model.QuestionItemModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Converter
{
    internal class IsCountCommentsToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var messageItem = parameter as AnswerItem;

            if (messageItem.CountComments - messageItem.Comments.Count != 0)
                return true;

            //if (int.Parse(value.ToString()) > 0)
            //    return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
