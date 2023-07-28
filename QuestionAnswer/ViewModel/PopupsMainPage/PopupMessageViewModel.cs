using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.ViewModel.PopupsMainPage
{
    public partial class PopupMessageViewModel : ObservableObject
    {
        public PopupMessageViewModel(string title, string description)
        {
            Title = title;
            Description = description;
        }

        [ObservableProperty]
        public string title;

        [ObservableProperty]
        public string description;

        [RelayCommand]
        public async void PopupClose()
        {
            await MopupService.Instance.RemovePageAsync(MopupService.Instance.PopupStack[0]);
        }
    }
}
