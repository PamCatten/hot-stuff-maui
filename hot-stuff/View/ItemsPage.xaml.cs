using InputKit.Shared.Controls;
using UraniumUI.Pages;

namespace HotStuff.View;

public partial class ItemsPage : UraniumContentPage
{
    public ItemsPage(ItemsPageViewModel vm)
    {
        SelectionView.GlobalSetting.CornerRadius = 0;
        InitializeComponent();
        BindingContext = vm;
    }

    async void OnAddItemsPageClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("----User clicked add icon");
        await Shell.Current.GoToAsync(nameof(AddItemsPage));
    }

    async void OnExportPageClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("----User clicked export icon.");

        await Shell.Current.GoToAsync(nameof(ExportPage));
    }

    async void OnProfilePageClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("----User clicked profile icon.");

        await Shell.Current.GoToAsync(nameof(ProfilePage));
    }
}
