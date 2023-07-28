using QuestionAnswer.ViewModel;
using QuestionAnswer.ViewModel.PopupsMainPage;

namespace QuestionAnswer.View.PopupsMainPage;

public partial class PopupNotificationView
{
	public PopupNotificationView()
	{
		InitializeComponent();

		this.BindingContext = new PopupNotificationQuestionViewModel();
	}
}