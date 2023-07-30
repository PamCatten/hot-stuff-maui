using UraniumUI.Pages;

namespace HotStuff.View;

public partial class LearnPage : UraniumContentPage
{
	public LearnPage()
	{
		InitializeComponent();
	}

    async void OnProfilePageClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("User clicked ProfilePage link.");

        await Shell.Current.GoToAsync(nameof(ProfilePage));
    }

}