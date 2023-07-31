using UraniumUI.Pages;
using Plugin.Media;
using Plugin.Media.Abstractions;
namespace HotStuff.View;

public partial class AddItemsPage : UraniumContentPage
{
    public AddItemsPage(AddItemsPageViewModel addItemsPageViewModel)
    {
        InitializeComponent();
        BindingContext = addItemsPageViewModel;
    }

    private async void OnPickPhotoClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        { 
            PickerTitle = "Select Image",
            FileTypes = FilePickerFileType.Images

        });

        if (result is null) return;

        PurchaseProof.Text = result?.FullPath;
        //var stream = await result.OpenReadAsync();
        //ItemImage.Source = ImageSource.FromStream(() => stream);

        // First implementation
        //var result = await CrossMedia.Current.PickPhotoAsync();
        //if (result is null) return;

        //ItemImage.Source = result?.Path;

        //var fileInfo = new FileInfo(result?.Path);
        //var fileLength = fileInfo.Length;
    }

    private async void OnTakePhotoClicked(object sender, EventArgs e)
    {
        var options = new StoreCameraMediaOptions { CompressionQuality = 100 };
        var result = await CrossMedia.Current.TakePhotoAsync(options);
        if (result is null) return;
    }
}
