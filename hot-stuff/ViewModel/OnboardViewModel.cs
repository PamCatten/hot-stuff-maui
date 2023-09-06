using Mopups.Services;
using System.Windows.Input;

namespace HotStuff.ViewModel;

public partial class OnboardViewModel : ObservableObject
{
    public ICommand ClosePopupCommand { get; protected set; }
    public ObservableCollection<OnboardScreen> OnboardScreens { get; set; } = new ObservableCollection<OnboardScreen>();
    public OnboardViewModel()
    {
        ClosePopupCommand = new Command(() => ClosePopup());

        OnboardScreens.Add(new OnboardScreen
        {
            OnboardTitle = "Order Your Food",
            OnboardDescription = "Now you can order food anytime right from mobile",
        });

        OnboardScreens.Add(new OnboardScreen
        {
            OnboardTitle = "Order Your Food",
            OnboardDescription = "Now you can order food anytime right from mobile",
        });

        OnboardScreens.Add(new OnboardScreen
        {
            OnboardTitle = "Order Your Food",
            OnboardDescription = "Now you can order food anytime right from mobile",
        });

        async void ClosePopup() { await MopupService.Instance.PopAsync(); }
    }
}
