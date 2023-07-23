using HotStuff.Pages;
using UraniumUI.Material.Controls;

namespace HotStuff;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();


        Routing.RegisterRoute(nameof(AddItemsPage), typeof(AddItemsPage));
    }
}