using InputKit.Shared.Controls;
using UraniumUI.Pages;

namespace HotStuff.View;

public partial class ItemsPage : UraniumContentPage
{
    //public ObservableCollection<Item> ItemManifest { get; set; } = new();
    public ItemsPage(ItemsPageViewModel vm)
    {
        SelectionView.GlobalSetting.CornerRadius = 0;
        InitializeComponent();
        BindingContext = vm;
    }
    protected async override void OnAppearing()
    {
        /* IN CASE OF EMERGENCY */ // await App.ItemService.FlushItems();
        // UraniumGrid.ItemsSource = await App.ItemService.GetItems();
        base.OnAppearing();
        //var items = await App.ItemService.GetItems();
        //MainThread.BeginInvokeOnMainThread(async () =>
        //{
            //ItemManifest.Clear();
            //Debug.WriteLine("Cleared items.");
            //foreach (var item in items)
            //{
                //ItemManifest.Add(item);
                //Debug.WriteLine($"Added {item.ItemID}, {item.ItemName}");
            //}
            //Debug.WriteLine("Completed loop.");
        //});
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
