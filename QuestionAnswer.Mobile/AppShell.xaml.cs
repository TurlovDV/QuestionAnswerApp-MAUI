using QuestionAnswer.Mobile.View;
using QuestionAnswer.Mobile.ViewModel;

namespace QuestionAnswer.Mobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(MessagePageView), typeof(MessagePageView));
		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(RegistrationPageView), typeof(RegistrationPageView));
        Routing.RegisterRoute(nameof(ProfilePageView), typeof(ProfilePageView));
    }
}
