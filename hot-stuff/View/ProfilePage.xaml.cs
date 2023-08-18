using UraniumUI.Pages;
namespace HotStuff.View;

public partial class ProfilePage : UraniumContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
        BindingContext = new ProfilePageViewModel();
    }

    async void BuildingSettingsCommand(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(BuildingSettingsPage));
    }

}
