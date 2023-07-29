using QuestionAnswer.Mobile.ViewModel;

namespace QuestionAnswer.Mobile.View;

public partial class ProfilePageView : ContentPage
{
	public ProfilePageView()
	{
		InitializeComponent();

		this.BindingContext = new ProfilePageViewModel();
	}
}