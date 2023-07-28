using QuestionAnswer.ViewModel.PopupsMainPage;

namespace QuestionAnswer.View.PopupsMainPage;

public partial class PopupMessageView
{
	public PopupMessageView(string title, string description)
	{
		InitializeComponent();

		this.BindingContext = new PopupMessageViewModel(title, description);
	}
}