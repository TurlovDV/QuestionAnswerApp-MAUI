using QuestionAnswer.ViewModel;

namespace QuestionAnswer.View;

public partial class ProfilePageView : ContentPage
{
	public ProfilePageView()
	{
		InitializeComponent();

		this.BindingContext = new ProfilePageViewModel();
	}
}