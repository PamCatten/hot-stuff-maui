﻿using HotStuff.Models;
using InputKit.Shared.Controls;
using System.Diagnostics;
using UraniumUI.Pages;
namespace HotStuff.Pages;

public partial class ItemsPage : UraniumContentPage
{
    public ItemsPage(ItemsPageViewModel vm)
    {
        SelectionView.GlobalSetting.CornerRadius = 0;
        InitializeComponent();
        BindingContext = vm;
    }

    async void OnProfilePageClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("User clicked ProfilePage link.");

        await Shell.Current.GoToAsync(nameof(ProfilePage));
    }

}
