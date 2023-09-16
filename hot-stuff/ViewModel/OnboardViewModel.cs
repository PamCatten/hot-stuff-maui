﻿using HotStuff.Services;
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
    public ICommand OpenAddBuildingPopupCommand { get; protected set; }
    public ICommand ClosePopupCommand { get; protected set; }
    public ICommand OnboardPositionCommand => new Command(async () =>
    {
        if (Position >= OnboardScreens.Count - 1)
        { 
            await MopupService.Instance.PopAllAsync();
            await MopupService.Instance.PushAsync(new AddBuildingPopup(buildingService));
        }
        else
        { position += 1; }
    });
    public ObservableCollection<OnboardScreen> OnboardScreens { get; set; } = new ObservableCollection<OnboardScreen>();
    public OnboardViewModel()
    {
        OpenLegalPopupCommand = new Command(async () => await MopupService.Instance.PushAsync(new LegalPopup(buildingService)));
        OpenOnboardCarouselPopupCommand = new Command(async () => await MopupService.Instance.PushAsync(new OnboardCarouselPopup(buildingService)));
        OpenAddBuildingPopupCommand = new Command(async () => await MopupService.Instance.PushAsync(new AddBuildingPopup(buildingService)));
        ClosePopupCommand = new Command(async () => await MopupService.Instance.PopAsync());

        OnboardScreens.Add(new OnboardScreen
        {
            OnboardTitle = "Organized, secure",
            OnboardDescription = "Welcome to Hot Stuff! Your trusted companion in making proof-of-loss fire claims quick and easy. Add photos, descriptions, and values to ensure you're prepared for the unexpected.",
            OnboardImage = "onboard1"
        });

        OnboardScreens.Add(new OnboardScreen
        {
            OnboardTitle = "Seamless management",
            OnboardDescription = "Effortlessly download and transfer your records to Excel or Google Sheets for added convenience. You're in control of your data, always.",
            OnboardImage = "onboard2"
        });

        OnboardScreens.Add(new OnboardScreen
        {
            OnboardTitle = "Privacy first",
            OnboardDescription = "Designed with anonymity in mind, we never collect any of your personal information. Your data stays private, and we're here to help you protect it.",
            OnboardImage = "onboard3"
        });

        OnboardScreens.Add(new OnboardScreen
        {
            OnboardTitle = "Peace of mind, simplified",
            OnboardDescription = "Rest easy knowing that if a fire breaks out, your records are secured digitally, so you are free to focus all of your attention on protecting what matters most.",
            OnboardImage = "onboard4"
        });
    }
}
