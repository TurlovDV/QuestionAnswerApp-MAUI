using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific;
using Mopups.Services;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.DTO.Services;
using QuestionAnswer.Mobile.Model.QuestionItemModel;
using QuestionAnswer.Services;
using QuestionAnswer.View.PopupsMainPage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application = Microsoft.Maui.Controls.Application;

namespace QuestionAnswer.ViewModel
{
    public partial class MessagePageViewModel : ObservableObject
    {
        private IDataCentreAppService dataCentreService;
        private IStorageOptionsService storageOptionsService;

        [ObservableProperty]
        public MessageItem replyToMessage;

        [ObservableProperty]
        QuestionItem questionItem;

        [ObservableProperty]
        string entryAnswerText;

        public MessagePageViewModel(QuestionItem questionItem)
        {
            storageOptionsService = ServiceProvider.GetService<IStorageOptionsService>();
            dataCentreService = ServiceProvider.GetService<IDataCentreAppService>();
            this.questionItem = questionItem;

            QuestionItem.Answers = new();
            
            Application.Current.Dispatcher.DispatchAsync(async () => await LoadingAnswers(0));            
        }

        [RelayCommand]
        public async void GoToMainPage()
        {
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public void ShowAnswers()
        {
            Application.Current.Dispatcher.DispatchAsync(async () => await LoadingAnswers(QuestionItem.Answers.Count));
        }
        [RelayCommand]
        public void ShowComments(object answerItem)
        {
            Application.Current.Dispatcher.DispatchAsync(async () => await LoadingCommentsToAnswers(answerItem as AnswerItem));
        }

        [RelayCommand]
        public void SendReplyToMessage(MessageItem messageItem)
        {
            ReplyToMessage = messageItem;
        }

        [RelayCommand]
        public void CancelReplyToMessage()
        {
            ReplyToMessage = null;
        }

        [RelayCommand]
        public async void SendAnswer()
        {
            if (ReplyToMessage is AnswerItem)
            {
                await SendMessageToAnswer(ReplyToMessage.Id);

                (ReplyToMessage as AnswerItem).CountComments++;
            }
            else if (ReplyToMessage is MessageItem)
            {
                var answerItem = QuestionItem.Answers                
                    .FirstOrDefault(x => x.Comments.FirstOrDefault(m => m == ReplyToMessage) != null);

                await SendMessageToAnswer(answerItem.Id);

                answerItem.CountComments++;
            }
            else
            {
                await SendMessageToQuestions();

                QuestionItem.CountAnswers++;
            }
            
            await MopupService.Instance.PushAsync(new PopupMessageView("Отправлен ответ", EntryAnswerText));

            ReplyToMessage = null;
            EntryAnswerText = "";
        }


        private async Task SendMessageToAnswer(Guid messageItemId)
        {
            Guid idNewMessageItem = Guid.NewGuid();

            await dataCentreService.SendNewCommentToAnswer(new DTO.Model.CreateCommentToAnswer()
            {
                Id = idNewMessageItem,
                Description = EntryAnswerText,
                AnswerId = messageItemId,
                UserId = storageOptionsService.GetUserId()
            });

            var user = await dataCentreService.GetUser(storageOptionsService.GetUserId());

            QuestionItem.Answers.FirstOrDefault(x => x.Id == messageItemId).Comments
                .Add(new MessageItem()
                {
                    Id = idNewMessageItem,
                    Description = EntryAnswerText,
                    IsMy = true,
                    UserId = user.Id,
                    UserName = user.Name                    
                });
        }

        private async Task SendMessageToQuestions()
        {
            Guid idNewMessageItem = Guid.NewGuid();
            await dataCentreService.SendNewAnswerToQuestion(new DTO.Model.CreateAnswerToQuestion()
            {
                Id = idNewMessageItem,
                Description = EntryAnswerText,
                QuestionId = QuestionItem.Id,
                UserId = storageOptionsService.GetUserId()
            });

            var user = await dataCentreService.GetUser(storageOptionsService.GetUserId());
            
            QuestionItem.Answers.Add(new AnswerItem()
            {
                Description = EntryAnswerText,
                UserName = user.Name,
                IsMy = true,
                UserId = user.Id,
                Id = idNewMessageItem
            });
        }

        public async Task LoadingAnswers(int countStart)
        {
            var answers = await dataCentreService.GetAnswersToQuestion(storageOptionsService.GetUserId(), QuestionItem.Id, countStart);

            answers.ToList().ForEach(async x =>
            {
                x.Comments = new();
                await LoadingCommentsToAnswers(x);

                var userId = storageOptionsService.GetUserId();
                if (userId == x.UserId)
                    x.IsMy = true;

                QuestionItem.Answers.Add(x);
            });
        }

        [RelayCommand]
        public async void Like(object sender)
        {
            var messageItem = sender as MessageItem;

            if (messageItem.IsDizLike)
                DizLike(sender);

            var likeItem = new DTO.Model.LikeItem()
            {
                Id = Guid.NewGuid(),
                MessageId = messageItem.Id,
                UserId = storageOptionsService.GetUserId()
            };
            
            if (messageItem.IsLike)
            {
                await dataCentreService.MessageCancelLike(likeItem);
                messageItem.CountLike--;
            }
            else
            {
                await dataCentreService.MessageLike(likeItem);
                messageItem.CountLike++;
            }

            messageItem.IsLike = !messageItem.IsLike;            
        }

        [RelayCommand]
        public async void DizLike(object sender)
        {
            var messageItem = sender as MessageItem;

            if (messageItem.IsLike)
                Like(sender);

            var likeItem = new DTO.Model.LikeItem()
            {
                Id = Guid.NewGuid(),
                MessageId = messageItem.Id,
                UserId = storageOptionsService.GetUserId()
            };

            if (messageItem.IsDizLike)
            {
                await dataCentreService.MessageCancelDizLike(likeItem);
                messageItem.CountDizLike--;
            }
            else
            {
                await dataCentreService.MessageDizLike(likeItem);
                messageItem.CountDizLike++;
            }

            messageItem.IsDizLike = !messageItem.IsDizLike;
        }

        public async Task LoadingCommentsToAnswers(AnswerItem answerItem)
        {
            var comments = await dataCentreService.GetCommentsOfAnswer(storageOptionsService.GetUserId(), answerItem.Id, answerItem.Comments.Count);

            Guid userId = storageOptionsService.GetUserId();

            comments.ToList().ForEach(x =>
            {
                if (userId == x.UserId)
                    x.IsMy = true;
                answerItem.Comments.Add(x);
            });

            if (answerItem.CountComments - answerItem.Comments.Count == 0)
                answerItem.IsVisibleShowMore = false;
        }
    }
}
