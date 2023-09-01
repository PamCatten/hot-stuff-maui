using UraniumUI.Pages;

namespace HotStuff.View;
public partial class LearnPage : UraniumContentPage
{
	public LearnPage(LearnViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}