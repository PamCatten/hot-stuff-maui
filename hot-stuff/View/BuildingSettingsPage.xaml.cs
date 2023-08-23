using UraniumUI.Pages;

namespace HotStuff.View;
public partial class BuildingSettingsPage : UraniumContentPage
{
    public BuildingSettingsPage(BuildingSettingsPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
