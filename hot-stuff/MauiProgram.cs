using HotStuff.Model;
using HotStuff.View;
using HotStuff.Services;
using InputKit.Shared.Controls;
using Microsoft.Extensions.DependencyInjection;
using Mopups.Hosting;
using UraniumUI;
using CommunityToolkit.Maui;
using HotStuff.ViewModel;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace HotStuff;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureMopups().UseUraniumUI().UseUraniumUIMaterial().UseSkiaSharp(true).ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFont("OpenSans-Bold.ttf", "OpenSansBold");
            fonts.AddFont("OpenSans-BoldItalic.ttf", "OpenSansBoldItalic");
            fonts.AddFont("OpenSans-Italic.ttf", "OpenSansItalic");
            fonts.AddFont("OpenSans-Light.ttf", "OpenSansLight");
            fonts.AddFont("OpenSans-LightItalic.ttf", "OpenSansLightItalic");

            fonts.AddFontAwesomeIconFonts();
            fonts.AddFont("fa_solid.otf", "FontAwesome");
        }).ConfigureMauiHandlers(handlers => 
        {
            #if ANDROID
                handlers.AddHandler(typeof(Shell), typeof(Platforms.Android.CustomShellRenderer));
            #endif
        }).UseMauiCommunityToolkit();
            // Services
            builder.Services.AddSingleton<ItemService>();
            builder.Services.AddSingleton<BuildingService>();
            // Views
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<ItemsPage>();
            builder.Services.AddSingleton<LearnPage>();
            // Popups (Mopups)
            builder.Services.AddSingleton<AddBuildingPopup>();
            builder.Services.AddSingleton<AddItemPopup>();
            builder.Services.AddSingleton<BuildingSettingsPopup>();
            builder.Services.AddSingleton<CopyPopup>();
            builder.Services.AddSingleton<DeletePopup>();
            builder.Services.AddSingleton<DownloadPopup>();
            builder.Services.AddSingleton<LegalPopup>();
            builder.Services.AddSingleton<OnboardCarouselPopup>();
            builder.Services.AddSingleton<OnboardPopup>();
            builder.Services.AddSingleton<ProfilePopup>();
            builder.Services.AddSingleton<TransferPopup>();
            // ViewModels
            builder.Services.AddTransient<BaseViewModel>();
            builder.Services.AddTransient<ItemsPageViewModel>();
            builder.Services.AddTransient<ProfilePageViewModel>();
            builder.Services.AddTransient<OnboardViewModel>();

            string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "ItemData.db3");
            builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ItemService>(s, DatabasePath));
            builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<BuildingService>(s, DatabasePath));
            builder.Services.AddMopupsDialogs();
        return builder.Build();
    }
}

