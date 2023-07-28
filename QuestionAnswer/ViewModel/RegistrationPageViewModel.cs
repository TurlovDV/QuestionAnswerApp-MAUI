using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Handlers;
using Mopups.Services;
using QuestionAnswer.Api.Client;
using QuestionAnswer.DTO;
using QuestionAnswer.Services;
using QuestionAnswer.View;
using QuestionAnswer.View.PopupsMainPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.ViewModel
{    
    public partial class RegistrationPageViewModel : ObservableObject
    {
        private IInitializationUserService initializationUserService;
        private IStorageOptionsService storageOptionsService;


        [ObservableProperty]
        private string login;

        [ObservableProperty]
        private string password;

        public RegistrationPageViewModel()
        {
            storageOptionsService = ServiceProvider.GetService<IStorageOptionsService>();
            initializationUserService = ServiceProvider.GetService<IInitializationUserService>();
        }

        [RelayCommand]
        public async void RegistrationUser()
        {
            var authResponse = await initializationUserService.RegistrationUser(new DTO.Model.CreateNewUser()
            {
                AuthUserItem = new DTO.Model.AuthUserItem()
                {
                    Email = this.Login,
                    Password = this.Password
                },
                Id = Guid.NewGuid(),
                UserName = this.Login
            });

            if (authResponse is null)
            {
                await MopupService.Instance.PushAsync(new PopupMessageView("Ошибка", "Повторите попытку позже"));
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

            //await Shell.Current.GoToAsync(nameof(MainPage));
        }

        [RelayCommand]
        public void GoToLoginPage()
        {
            App.Current.MainPage = new LoginPageView();
            //await Shell.Current.GoToAsync("..");
        }
    }
}
