using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Compatibility;
using Mopups.Services;
using QuestionAnswer.Api.Client;
using QuestionAnswer.DTO;
using QuestionAnswer.Services;
using QuestionAnswer.View;
using QuestionAnswer.View.PopupsMainPage;
using QuestionAnswer.ViewModel.PopupsMainPage;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.ViewModel
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private IInitializationUserService initializationUserService;
        private IStorageOptionsService storageOptionsService;
        
        [ObservableProperty]
        private string login = "sFbtxdJlSl";

        [ObservableProperty]
        private string password = "3aqTb9sRL9";

        public LoginPageViewModel()
        {
            storageOptionsService = ServiceProvider.GetService<IStorageOptionsService>();
            initializationUserService = ServiceProvider.GetService<IInitializationUserService>();
        }

        [RelayCommand]
        public async void AuthenticationUser()
        {
            var authResponse = await initializationUserService.AuthenticationUser(new DTO.Model.AuthUserItem()
            {
                Email = this.Login,
                Password = this.Password
            });

            if (authResponse is null)
            {
                await MopupService.Instance.PushAsync(new PopupMessageView("Ошибка", "Проверьте введенные данные"));
                return;
            }

            var userId = Guid.Parse(DataCentreAppService.GetClaims(authResponse.RefreshToken)["name"]);

            storageOptionsService.SetRefreshToken(authResponse.RefreshToken);
            storageOptionsService.SetAccessToken(authResponse.AccessToken);
            storageOptionsService.SetUserId(userId);

            ApiConfiguration.AccessToken = authResponse.AccessToken;
            ApiConfiguration.RefreshToken = authResponse.RefreshToken;
            ApiConfiguration.UserId = userId;

            App.Current.MainPage = new AppShell();
        }

        [RelayCommand]
        public void GoToRegistrationPage()
        {
            App.Current.MainPage = new RegistrationPageView();
        }
    }
}
