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

        private void ShowBottomSheet(object sender, EventArgs e)
        {
            bottomSheet.IsPresented = true;
        }

        async void OnProfilePageClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Profile page clicked");

            await Shell.Current.GoToAsync(nameof(ProfilePage));
        }

    }
}