using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.Mobile.ViewModel;
using System.Net.Http.Json;

namespace QuestionAnswer.Mobile.View;

public partial class LoginPageView : ContentPage
{
	public LoginPageView()
	{
		InitializeComponent();

        this.BindingContext = new LoginPageViewModel();
    }
}