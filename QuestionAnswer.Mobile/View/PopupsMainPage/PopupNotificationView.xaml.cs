using QuestionAnswer.Mobile.ViewModel;
using QuestionAnswer.Mobile.ViewModel.PopupsMainPage;

namespace QuestionAnswer.Mobile.View.PopupsMainPage;

public partial class PopupNotificationView
{
	public PopupNotificationView()
	{
		InitializeComponent();

		this.BindingContext = new PopupNotificationQuestionViewModel();
	}
}