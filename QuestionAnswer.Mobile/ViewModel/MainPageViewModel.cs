﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using QuestionAnswer.Api.Client;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.DTO.Services;
using QuestionAnswer.Mobile.Model.QuestionItemModel;
using QuestionAnswer.Mobile.Services;
using QuestionAnswer.Mobile.View;
using QuestionAnswer.Mobile.View.PopupsMainPage;

namespace QuestionAnswer.Mobile.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        IDataCentreAppService dataCentreService;
        IStorageOptionsService storageOptionsService;

        [ObservableProperty]
        User profile;

        public ObservableCollection<QuestionItem> Questions { get; set; }

        [ObservableProperty]
        int positionQuestion;

        public MainPageViewModel()
        {
            dataCentreService = ServiceProvider.GetService<IDataCentreAppService>();
            storageOptionsService = ServiceProvider.GetService<IStorageOptionsService>();
            
            Questions = new();

            Application.Current.Dispatcher.DispatchAsync(async () =>
            {
                Profile = await dataCentreService.GetUser(storageOptionsService.GetUserId());

                await LoadingQuestions(Questions.Count);
            });
        }

        [RelayCommand]
        public async void GoToQuestionMessage(QuestionItem questionItemTap)
        {
            await Shell.Current.GoToAsync(nameof(MessagePageView), true,
                new Dictionary<string, object>
                {
                    ["QuestionItem"] = questionItemTap
                });
        }
        
        [RelayCommand]
        public async void GoToProfile()
        {
            await Shell.Current.GoToAsync(nameof(ProfilePageView));
        }

        [RelayCommand]
        public async void OpenPopupCreateQuestion()
        {
            await MopupService.Instance.PushAsync(new PopupCreateQuestionView());
        }

        [RelayCommand]
        public async void OpenPopupMyQuestion()
        {
            await MopupService.Instance.PushAsync(new PopupMyQuestionView());
        }

        [RelayCommand]
        public async void OpenPopupRatingInfo()
        {
            await MopupService.Instance.PushAsync(
                new PopupMessageView("Рейтинг", "Отвечай на вопросы и повышай свой рейтинг"));
        }

        [RelayCommand]
        public async void PositionQuestionChanged()
        {            
            if (PositionQuestion >= Questions.Count - 1)
            {
                await LoadingQuestions(Questions.Count);
            }

            await dataCentreService.AddViewQuestion(Questions[PositionQuestion].Id);
        }

        public async Task LoadingQuestions(int countStart)
        {
            var questions = await dataCentreService
                .GetRandomQuestions(storageOptionsService.GetUserId(), countStart);

            questions.ToList().ForEach(x => Questions.Add(x));
        }
    }
}