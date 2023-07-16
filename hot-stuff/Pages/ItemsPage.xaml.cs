using InputKit.Shared.Controls;
using System.Diagnostics;
using UraniumUI.Pages;
namespace hot_stuff.Pages;

public partial class ItemsPage : UraniumContentPage
{
    public ItemsPage()
    {
        SelectionView.GlobalSetting.CornerRadius = 0;
        InitializeComponent();
    }
}
