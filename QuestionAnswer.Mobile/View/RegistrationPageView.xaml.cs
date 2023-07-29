using QuestionAnswer.Mobile.ViewModel;

namespace QuestionAnswer.Mobile.View;

public partial class RegistrationPageView : ContentPage
{
	public RegistrationPageView()
	{
		InitializeComponent();

		this.BindingContext = new RegistrationPageViewModel();
	}
}