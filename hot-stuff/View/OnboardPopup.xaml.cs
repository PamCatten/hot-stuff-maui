using Mopups.Pages;
using HotStuff.Services;

namespace HotStuff.View;

public partial class OnboardPopup : PopupPage
{
	public OnboardPopup(BuildingService buildingService)
	{
		InitializeComponent();
        BindingContext = new ProfilePageViewModel(buildingService);
	}
}