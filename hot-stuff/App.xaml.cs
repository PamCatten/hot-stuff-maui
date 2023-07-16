using UraniumUI.Material.Resources;

namespace HotStuff;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}