using QuestionAnswer.Mobile.ViewModel;
using QuestionAnswer.Mobile.ViewModel.PopupsMainPage;

namespace QuestionAnswer.Mobile.View.PopupsMainPage;

public partial class PopupMyQuestionView
{
	public PopupMyQuestionView()
	{
		InitializeComponent();

		this.BindingContext = new PopupMyQuestionViewModel();
	}
}