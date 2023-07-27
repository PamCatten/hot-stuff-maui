using HotStuff.Models;
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
    protected async override void OnAppearing()
    {
        /* IN CASE OF EMERGENCY */ // await App.ItemServ.FlushItems();
        // UraniumGrid.ItemsSource = await App.ItemServ.GetItems();
        base.OnAppearing();
    }
    async void OnAddItemsPageClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("----User clicked add icon");
        await Shell.Current.GoToAsync(nameof(AddItemsPage));
    }

    async void OnProfilePageClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("----User clicked profile Page.");

        await Shell.Current.GoToAsync(nameof(ProfilePage));
    }
}
