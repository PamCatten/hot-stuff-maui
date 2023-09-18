using HotStuff.Services;
using UraniumUI.Pages;

namespace HotStuff.View;
public partial class LearnPage : UraniumContentPage
{
	public LearnPage(BaseViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}