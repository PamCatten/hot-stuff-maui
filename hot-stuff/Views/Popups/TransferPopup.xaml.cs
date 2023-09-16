using CommunityToolkit.Maui.Views;
using Mopups.Pages;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Mopups.Services;
using HotStuff.Services;

namespace HotStuff.View;

public partial class TransferPopup : PopupPage
{
	public TransferPopup(ItemService itemService)
	{
		InitializeComponent();
        BindingContext = new ItemsPageViewModel(itemService);
	}
}