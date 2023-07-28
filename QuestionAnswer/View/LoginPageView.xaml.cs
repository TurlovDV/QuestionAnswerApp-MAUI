using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.ViewModel;
using System.Net.Http.Json;

namespace QuestionAnswer.View;

public partial class LoginPageView : ContentPage
{
	public LoginPageView()
	{
		InitializeComponent();

        this.BindingContext = new LoginPageViewModel();
    }
}