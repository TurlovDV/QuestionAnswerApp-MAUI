using QuestionAnswer.ViewModel;

namespace QuestionAnswer.View;

public partial class RegistrationPageView : ContentPage
{
	public RegistrationPageView()
	{
		InitializeComponent();

		this.BindingContext = new RegistrationPageViewModel();
	}
}