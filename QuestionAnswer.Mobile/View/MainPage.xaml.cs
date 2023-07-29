using QuestionAnswer.Api.Client;
using QuestionAnswer.DTO.Services;
using QuestionAnswer.Mobile.ViewModel;

namespace QuestionAnswer.Mobile.View;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

        this.BindingContext = new MainPageViewModel();
	}

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}