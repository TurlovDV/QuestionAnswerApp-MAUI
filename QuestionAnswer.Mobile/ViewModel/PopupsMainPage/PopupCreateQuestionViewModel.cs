using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Services;
using QuestionAnswer.Mobile.Services;
using QuestionAnswer.Mobile.View.PopupsMainPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Mobile.ViewModel.PopupsMainPage
{
    public partial class PopupCreateQuestionViewModel : ObservableObject
    {
        IDataCentreAppService dataCentreService;
        IStorageOptionsService storageOptionsService;

        [ObservableProperty]
        public string title;

        [ObservableProperty]
        public string description;

        public PopupCreateQuestionViewModel()
        {
            storageOptionsService = ServiceProvider.GetService<IStorageOptionsService>();
            dataCentreService = ServiceProvider.GetService<IDataCentreAppService>();
        }

        [RelayCommand]
        public async Task SendNewQuestion()
        {
            await dataCentreService.CreateNewQuestion(new CreateQuestionItem()
            {
                  Description = Description,
                  Title = Title,
                  UserId = storageOptionsService.GetUserId(),
                  Id = Guid.NewGuid()
            });

            await MopupService.Instance.RemovePageAsync(MopupService.Instance.PopupStack[0]);

            await MopupService.Instance.PushAsync(new PopupMessageView("Вопрос задан", Title));
        }
    }
}
