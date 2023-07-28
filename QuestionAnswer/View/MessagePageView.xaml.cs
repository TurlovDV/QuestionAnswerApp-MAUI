using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.Mobile.Model.QuestionItemModel;
using QuestionAnswer.ViewModel;

namespace QuestionAnswer.View;


[QueryProperty(nameof(QuestionItem), "QuestionItem")]
public partial class MessagePageView : ContentPage
{
	QuestionItem questionItem;
	public QuestionItem QuestionItem 
	{
		set
		{
			questionItem = value;
            this.BindingContext = new MessagePageViewModel(questionItem);
        }
	}

	public MessagePageView()
	{
		InitializeComponent();		
	}
}