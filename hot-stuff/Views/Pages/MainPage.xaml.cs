using InputKit.Shared.Controls;
using UraniumUI.Pages;

namespace HotStuff;
public partial class MainPage : UraniumContentPage
{
    public MainPage(BaseViewModel vm)
    {
        SelectionView.GlobalSetting.CornerRadius = 0;
        InitializeComponent();
        BindingContext = vm;
    }
}