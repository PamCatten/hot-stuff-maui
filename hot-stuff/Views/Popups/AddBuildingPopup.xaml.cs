using Mopups.Pages;
using HotStuff.Services;

namespace HotStuff.View;

public partial class AddBuildingPopup : PopupPage
{
	public AddBuildingPopup(BuildingService buildingService)
	{
		InitializeComponent();
        BindingContext = new ProfilePageViewModel(buildingService);
	}
}