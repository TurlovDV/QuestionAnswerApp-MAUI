using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using QuestionAnswer.Api.Client;
using QuestionAnswer.DTO;
using QuestionAnswer.DTO.Services;
using QuestionAnswer.Mobile.Services;
using QuestionAnswer.Mobile.View;
using QuestionAnswer.Mobile.ViewModel;
using System.Net;

namespace QuestionAnswer.Mobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.RegisterServices()
			.RegisterViewModels()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Roboto-Medium.ttf", "Roboto-Medium");
            });


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
	}

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
	{
        mauiAppBuilder.Services.AddSingleton<LoginPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<RegistrationPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<MainPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<MessagePageViewModel>();
        return mauiAppBuilder;
	}

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IDataCentreAppService, DataCentreAppService>();
		mauiAppBuilder.Services.AddSingleton<IInitializationUserService, InitializationUserService>();
        mauiAppBuilder.Services.AddSingleton<IStorageOptionsService, StorageOptionsService>();
        return mauiAppBuilder;
    }
}
