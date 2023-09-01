using HotStuff.Services;
using Mopups.Services;
using System.Windows.Input;

namespace HotStuff.ViewModel;

public partial class LearnViewModel : BaseViewModel
{
    readonly BuildingService buildingService;
    public ICommand ClosePopupCommand { get; protected set; }
    public ICommand OpenProfilePopupCommand { get; protected set; }
    public LearnViewModel()
    {
        ClosePopupCommand = new Command(() => ClosePopup());
        OpenProfilePopupCommand = new Command(async () => await MopupService.Instance.PushAsync(new ProfilePopup(buildingService)));

        async void ClosePopup() { await MopupService.Instance.PopAsync(); }
    }
}
