using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.DTO.Services;
using QuestionAnswer.Mobile.Model.QuestionItemModel;
using QuestionAnswer.Services;
using QuestionAnswer.View;
using QuestionAnswer.View.PopupsMainPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.ViewModel.PopupsMainPage
{
    public partial class PopupMyQuestionViewModel : ObservableObject
    {
        IDataCentreAppService dataCentreService;
        IStorageOptionsService storageOptionsService;
        public ObservableCollection<QuestionItem> MyQuestions { get; set; }

        [ObservableProperty]
        public int countMyQyestions;

        public PopupMyQuestionViewModel()
        {
            storageOptionsService = ServiceProvider.GetService<IStorageOptionsService>();
            dataCentreService = ServiceProvider.GetService<IDataCentreAppService>();

            MyQuestions = new ObservableCollection<QuestionItem>();

            Application.Current.Dispatcher.DispatchAsync(async () =>
            {
                var user = await dataCentreService.GetUser(storageOptionsService.GetUserId());
                CountMyQyestions = user.CountMyQuestions;

                await LoadingQuestions(MyQuestions.Count);
            });
        }

        [RelayCommand]
        public async void CloseThisPopup()
        {
            await MopupService.Instance.PopAsync();
        }

        [RelayCommand]
        public void RemainingQuestions()
        {
            Application.Current.Dispatcher.DispatchAsync(async () =>
            {
                await LoadingQuestions(MyQuestions.Count);
            });
        }

        public async Task LoadingQuestions(int startCount)
        {            
            var questions = await dataCentreService.GetQuestionsFromUser(storageOptionsService.GetUserId(), startCount);

            questions.ToList().ForEach(x => MyQuestions.Add(x));
        }

        [RelayCommand]
        public async void GoToQuestionMessage(QuestionItem questionItemTap)
        {
            await MopupService.Instance.PopAsync();
         
            await Shell.Current.GoToAsync(nameof(MessagePageView), true,
                new Dictionary<string, object>
                {
                    ["QuestionItem"] = questionItemTap
                });
        }

        [RelayCommand]
        public async void DeleteQuestion(QuestionItem questionItem)
        {
            await dataCentreService.DeleteQuestion(questionItem.Id);
            
            MyQuestions.Remove(questionItem);
            CountMyQyestions--;
            //ServiceProvider.GetService<MainPageViewModel>().Profile.CountMyQuestions--;

            await MopupService.Instance.PushAsync(new PopupMessageView("Вопрос удален", $"Вопрос {questionItem.Description}"));
        }
    }
}
