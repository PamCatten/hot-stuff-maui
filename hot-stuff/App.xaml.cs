using HotStuff.Services;
using UraniumUI.Material.Resources;

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