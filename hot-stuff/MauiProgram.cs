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

            fonts.AddFont("Montserrat-Regular.ttf", "MontserratRegular");
            fonts.AddFont("Montserrat-SemiBold.ttf", "MontserratSemiBold");
            fonts.AddFont("Montserrat-Bold.ttf", "MontserratBold");
            fonts.AddFont("Montserrat-BoldItalic.ttf", "MontserratBoldItalic");
            fonts.AddFont("Montserrat-Italic.ttf", "MontserratItalic");
            fonts.AddFont("Montserrat-Light.ttf", "MontserratLight");
            fonts.AddFont("Montserrat-LightItalic.ttf", "MontserratLightItalic");

            fonts.AddFontAwesomeIconFonts();
            fonts.AddFont("fa_solid.otf", "FontAwesome");

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