using QuestionAnswer.Api.Client;
using QuestionAnswer.DTO.Services;
using QuestionAnswer.ViewModel;

namespace QuestionAnswer.View;

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