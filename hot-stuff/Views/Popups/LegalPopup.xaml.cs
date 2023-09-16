using Mopups.Pages;
using HotStuff.Services;

namespace HotStuff.View;

public partial class LegalPopup : PopupPage
{
	public LegalPopup(BuildingService buildingService)
	{
		InitializeComponent();
		BindingContext = new ProfilePageViewModel(buildingService);
	}
}