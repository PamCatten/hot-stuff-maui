using hot_stuff.Pages;
using InputKit.Shared.Controls;
using System.Diagnostics;
using UraniumUI.Material.Controls;
using UraniumUI.Pages;

namespace hot_stuff
{
    public partial class MainPage : UraniumContentPage
    {
        public MainPage()
        {
            SelectionView.GlobalSetting.CornerRadius = 0;
            InitializeComponent();
        }

        async void OnProfilePageClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("User clicked ProfilePage link.");

            await Shell.Current.GoToAsync(nameof(ProfilePage));
        }

    }
}