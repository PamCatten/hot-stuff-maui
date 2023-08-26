namespace HotStuff;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(BuildingSettingsPage), typeof(BuildingSettingsPage));
    }
}