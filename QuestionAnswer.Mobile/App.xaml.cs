using Microsoft.Maui.Platform;
using QuestionAnswer.Api.Client;
using QuestionAnswer.Mobile.Services;
using QuestionAnswer.Mobile.View;

namespace QuestionAnswer.Mobile;

public partial class App : Application
{    
	public App()
	{
        //Preferences.Clear();

		InitializeComponent();

        IStorageOptionsService storageOptions = ServiceProvider.GetService<IStorageOptionsService>();

        if (storageOptions.GetUserId() != Guid.Empty)
        {
            ApiConfiguration.RefreshToken = storageOptions.GetRefreshToken();
            ApiConfiguration.UserId = storageOptions.GetUserId();
            MainPage = new AppShell();
        }
        else
            MainPage = new LoginPageView();

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.TextCursorDrawable.SetTint(Colors.White.ToPlatform());
            //handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS || MACCATALYST
         
#elif WINDOWS
      
#endif
        });
    }
}
