using Mopups.Services;
using System.Windows.Input;

namespace HotStuff.ViewModel;

public partial class OnboardViewModel : ObservableObject
{
    public ICommand ClosePopupCommand { get; protected set; }
    public ObservableCollection<OnboardScreen> OnboardScreens { get; set; } = new ObservableCollection<OnboardScreen>();
    private string buttonText = "Next";
    public string ButtonText
    {
        get => buttonText;
        set => SetProperty(ref buttonText, value);
    }
    public ICommand OnboardPositionCommand => new Command(async () =>
    {
        if (Position == OnboardScreens.Count - 1)
        {
            await MopupService.Instance.PopAsync();
        }
        else
        {
            position += 1;
        }
    });

    private int position = 0;
    public int Position { get => position; set { position = value; OnPropertyChanged(); } }
    public OnboardViewModel()
    {
        ClosePopupCommand = new Command(() => ClosePopup());

        OnboardScreens.Add(new OnboardScreen
        {
            OnboardTitle = "Onboard Title 1",
            OnboardDescription = "Onboard Description 1",
            OnboardImageLight = "onboardlight",
            OnboardImageDark = "onboarddark"
        });

        OnboardScreens.Add(new OnboardScreen
        {
            OnboardTitle = "Onboard Title 2",
            OnboardDescription = "Onboard Description 2",
            OnboardImageLight = "onboardlight",
            OnboardImageDark = "onboarddark"
        });

        OnboardScreens.Add(new OnboardScreen
        {
            OnboardTitle = "Onboard Title 3",
            OnboardDescription = "Onboard Description 3",
            OnboardImageLight = "onboardlight",
            OnboardImageDark = "onboarddark"
        });

        async void ClosePopup() { await MopupService.Instance.PopAsync(); }

    }
}
