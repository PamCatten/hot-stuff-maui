using HotStuff.Services;

namespace HotStuff;

public partial class App : Application
{
    public static ItemService ItemService {get; private set;}
    public App(ItemService itemservice)
    {
        InitializeComponent();

        MainPage = new AppShell();

        ItemService = itemservice;
    }
}