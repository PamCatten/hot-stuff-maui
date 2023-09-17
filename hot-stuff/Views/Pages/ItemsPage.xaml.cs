using InputKit.Shared.Controls;
using UraniumUI.Pages;

namespace HotStuff.View;
public partial class ItemsPage : UraniumContentPage
{
    public ItemsPage(ItemsPageViewModel vm)
    {
        SelectionView.GlobalSetting.CornerRadius = 0;
        InitializeComponent();
        BindingContext = vm;
    }
}
