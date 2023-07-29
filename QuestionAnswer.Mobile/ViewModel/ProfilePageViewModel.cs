using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Services;
using QuestionAnswer.Mobile.Services;
using QuestionAnswer.Mobile.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Mobile.ViewModel
{
    public partial class ProfilePageViewModel : ObservableObject
    {
        private IStorageOptionsService storageOptionsService;
        private IDataCentreAppService dataCentreService;

        [ObservableProperty]
        User myProfile;

        public ProfilePageViewModel()
        {
            storageOptionsService = ServiceProvider.GetService<IStorageOptionsService>();
            dataCentreService = ServiceProvider.GetService<IDataCentreAppService>();

            Application.Current.Dispatcher.DispatchAsync(async () =>
            {
                await LoadingUser(storageOptionsService.GetUserId());
            });
        }

        [RelayCommand]
        public async void GoToMainPage()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async void GoToBack()
        {
            await Shell.Current.GoToAsync(nameof(LoginPageView));
        }

        [RelayCommand]
        public void Exit()
        {
            Preferences.Clear();
            App.Current.MainPage = new LoginPageView();
        }

        public async Task LoadingUser(Guid userId)
        {
            MyProfile = await dataCentreService.GetUser(userId);
        }
    }
}
