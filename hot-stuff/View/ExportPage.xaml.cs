using System.IO;
using CsvHelper;
using System.Globalization;

namespace HotStuff.View;

public partial class ExportPage : UraniumUI.Pages.UraniumContentPage
{
    public ExportPage()
    {
        InitializeComponent();

    }

    async void OnMakeCSVClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("----User clicked make CSV button.");
        var csvPath = Path.Combine($@"{Environment.CurrentDirectory}", $"items-{DateTime.Now.ToFileTime}.csv");
        Debug.WriteLine($"Path: {csvPath}");
        try
        {
            using (var streamWriter = new StreamWriter(csvPath))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    var items = App.ItemService.GetItems();
                    csvWriter.WriteRecords((System.Collections.IEnumerable)items);
                }
            }
            Debug.WriteLine("----Made CSV.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Something went wrong: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Transfer Error", ex.Message, "OK");
        }
    }

}
