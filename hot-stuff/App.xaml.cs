using UraniumUI.Material.Resources;

namespace hot_stuff;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}