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
            OnboardTitle = "Conveniently manage your possessions",
            OnboardDescription = "Viamusa ac liquola augue luctus ulfratum praesant tempus mi enim at phaentra tempus mi enum at lineuk quam pelientseque non.",
            OnboardImage = "onboarddark"
        });

        OnboardScreens.Add(new OnboardScreen
        {
            OnboardTitle = "Download info easily to Excel or Sheets",
            OnboardDescription = "Viamusa ac liquola augue luctus ulfratum praesant tempus mi enim at phaentra tempus mi enum at lineuk quam pelientseque non.",
            OnboardImage = "onboarddark"
        });

        OnboardScreens.Add(new OnboardScreen
        {
            OnboardTitle = "Completely free, entirely anonymous",
            OnboardDescription = "Your records are private, no name, email, or credit card required.",
            OnboardImage = "onboarddark"
        });
    }
}
