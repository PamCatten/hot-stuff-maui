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
                handlers.AddHandler(typeof(Shell), typeof(HotStuff.Platforms.Android.CustomShellRenderer));
            #endif
        }).UseMauiCommunityToolkit();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<ItemsPage>();
            builder.Services.AddTransient<ItemsPageViewModel>();
            builder.Services.AddTransient<AddItemsPage>();
            builder.Services.AddTransient<AddItemsPageViewModel>();
            builder.Services.AddSingleton<ItemService>();



        string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "ItemData.db3");
            builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ItemService>(s, DatabasePath));
            builder.Services.AddMopupsDialogs();
        return builder.Build();
    }
}

