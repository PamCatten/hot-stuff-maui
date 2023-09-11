using HotStuff.Services;
using Mopups.Services;
using System.Windows.Input;

namespace HotStuff.ViewModel;
public partial class OnboardViewModel : BaseViewModel
{
    readonly BuildingService buildingService;
    private int position = 0;
    public int Position { get => position; set { position = value; OnPropertyChanged(); } }
    public ICommand OpenLegalPopupCommand { get; protected set; }
    public ICommand OpenOnboardCarouselPopupCommand { get; protected set; }
    public ICommand ClosePopupCommand { get; protected set; }
    public ICommand OnboardPositionCommand => new Command(async () =>
    {
        if (Position >= OnboardScreens.Count - 1)
        { await MopupService.Instance.PopAllAsync(); }
        else
        { position += 1; }
    });
    public ObservableCollection<OnboardScreen> OnboardScreens { get; set; } = new ObservableCollection<OnboardScreen>();
    public OnboardViewModel()
    {
        OpenLegalPopupCommand = new Command(async () => await MopupService.Instance.PushAsync(new LegalPopup(buildingService)));
        OpenOnboardCarouselPopupCommand = new Command(async () => await MopupService.Instance.PushAsync(new OnboardCarouselPopup(buildingService)));
        ClosePopupCommand = new Command(async () => await MopupService.Instance.PopAsync());

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
    }
}
