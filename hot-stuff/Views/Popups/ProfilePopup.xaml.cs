using Mopups.Pages;
using HotStuff.Services;

namespace HotStuff.View;

public partial class ProfilePopup : PopupPage
{
	public ProfilePopup(BuildingService buildingService)
	{
		InitializeComponent();
        BindingContext = new ProfilePageViewModel(buildingService);
	}
}