﻿using HotStuff.Models;
using HotStuff.Pages;
using InputKit.Shared.Controls;
using Microsoft.Extensions.DependencyInjection;
using Mopups.Hosting;
using UraniumUI;

namespace HotStuff;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureMopups()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFontAwesomeIconFonts();
                fonts.AddFont("fa_solid.otf", "FontAwesome");
            });

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<ItemsPage>();
        builder.Services.AddTransient<ItemsPageViewModel>();

        builder.Services.AddMopupsDialogs();
        return builder.Build();
    }
}