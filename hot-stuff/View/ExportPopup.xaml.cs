using CommunityToolkit.Maui.Views;
using Mopups.Pages;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Mopups.Services;
using HotStuff.Services;

namespace HotStuff.View;

public partial class ExportPopup : PopupPage
{
	public ExportPopup(ItemService itemService)
	{
		InitializeComponent();
        BindingContext = new ItemsPageViewModel(itemService);
	}
}