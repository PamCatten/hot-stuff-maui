using CommunityToolkit.Maui.Views;
using Mopups.Pages;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Mopups.Services;
using HotStuff.Services;

namespace HotStuff.View;

public partial class BuildingSettingsPopup : PopupPage
{
	public BuildingSettingsPopup(BuildingService buildingService)
	{
		InitializeComponent();
        BindingContext = new ProfilePageViewModel(buildingService);
	}
}