using CsvHelper;
using InputKit.Shared.Controls;
using System.Globalization;
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
        await Shell.Current.GoToAsync(nameof(AddItemsPage));
    }

    async void OnExportIconClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("----User clicked export icon.");
        var csvPath = Path.Combine($@"{Environment.CurrentDirectory}", $"items-{DateTime.Now.ToFileTime}.csv");
        Debug.WriteLine($"Path: {csvPath}");
        try
        {
            using (var streamWriter = new StreamWriter(csvPath))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                var items = App.ItemService.GetItems();
                csvWriter.WriteRecords((System.Collections.IEnumerable)items);
            }
            Debug.WriteLine("----Made CSV.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Something went wrong: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Transfer Error", ex.Message, "OK");
        }
    }

    async void OnProfilePageClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProfilePage));
    }
}
