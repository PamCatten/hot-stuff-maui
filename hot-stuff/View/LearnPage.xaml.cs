using HotStuff.Services;
using UraniumUI.Pages;

namespace HotStuff.View;
public partial class LearnPage : UraniumContentPage
{
	public LearnPage()
	{
		InitializeComponent();
		BindingContext = new LearnViewModel();
	}
}