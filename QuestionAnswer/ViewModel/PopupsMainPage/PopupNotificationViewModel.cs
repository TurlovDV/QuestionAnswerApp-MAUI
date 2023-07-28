using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using QuestionAnswer.DTO.Services;
using QuestionAnswer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notification = QuestionAnswer.DTO.Model.Notification;

namespace QuestionAnswer.ViewModel.PopupsMainPage
{
    public partial class PopupNotificationQuestionViewModel : ObservableObject
    {
        IDataCentreAppService dataCentre;
        
        public ObservableCollection<Notification> Notifications { get; set; }

        public PopupNotificationQuestionViewModel()
        {
            this.dataCentre = ServiceProvider.GetService<IDataCentreAppService>();

            Notifications = new ObservableCollection<Notification>(dataCentre.GetNotificaation().Result);

            /*
            Notifications = new ObservableCollection<Notification>()
            {
                new()
                {
                    Title = "Iroee",
                    Description = "Hello"
                },
                new()
                {
                    Title = "На ваш вопрос ответили",
                    Description = "Скольк стоит уцауц шоауц ?"
                },
                new()
                {
                    Title = "А как оно работает",
                    Description = "Да можно сделать"
                },
                new()
                {
                    Title = "Новый пользователь",
                    Description = "Как оно работает"
                },
                new()
                {
                    Title = "ккупк",
                    Description = "уцпуцпцпцпуцуп"
                },
            };
            */
        }

        [RelayCommand]
        public async void CloseThisPopup()
        {
            await MopupService.Instance.PopAsync();
        }
    }
}
