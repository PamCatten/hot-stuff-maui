using UraniumUI.Pages;
namespace HotStuff.View;

public partial class ProfilePage : UraniumContentPage
{
    public ProfilePage(ProfilePageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
