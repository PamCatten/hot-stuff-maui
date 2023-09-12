using Mopups.Pages;
using HotStuff.Services;

namespace HotStuff.View;

public partial class OnboardCarouselPopup : PopupPage
{
	public OnboardCarouselPopup(BuildingService buildingService)
	{
		InitializeComponent();
        BindingContext = new OnboardViewModel();
	}
}