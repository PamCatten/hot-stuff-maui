using Mopups.Pages;
using HotStuff.Services;
using HotStuff.ViewModel;

namespace HotStuff.View;

public partial class OnboardPopup : PopupPage
{
	public OnboardPopup()
	{
		InitializeComponent();
        BindingContext = new OnboardViewModel();
	}
}