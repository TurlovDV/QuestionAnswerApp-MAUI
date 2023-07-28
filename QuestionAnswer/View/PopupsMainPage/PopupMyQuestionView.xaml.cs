using QuestionAnswer.ViewModel;
using QuestionAnswer.ViewModel.PopupsMainPage;

namespace QuestionAnswer.View.PopupsMainPage;

public partial class PopupMyQuestionView
{
	public PopupMyQuestionView()
	{
		InitializeComponent();

		this.BindingContext = new PopupMyQuestionViewModel();
	}
}