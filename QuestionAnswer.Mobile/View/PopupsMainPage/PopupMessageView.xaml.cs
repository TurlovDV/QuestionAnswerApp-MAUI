using QuestionAnswer.Mobile.ViewModel.PopupsMainPage;

namespace QuestionAnswer.Mobile.View.PopupsMainPage;

public partial class PopupMessageView
{
	public PopupMessageView(string title, string description)
	{
		InitializeComponent();

		this.BindingContext = new PopupMessageViewModel(title, description);
	}
}