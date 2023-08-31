﻿using HotStuff.Services;
using Mopups.Services;
using System.Windows.Input;

namespace HotStuff.ViewModel;

public partial class ProfilePageViewModel : BaseViewModel
{
    readonly BuildingService buildingService;
    private Building newBuilding = new();
    public Building NewBuilding { get => newBuilding; set { newBuilding = value; OnPropertyChanged(); } }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    [ObservableProperty]
    bool isRefreshing;

    private ObservableCollection<Building> buildingList = new();
    public ObservableCollection<Building> BuildingList
    {
        get
        {
            return buildingList;
        }
        set
        {
            buildingList = value;
            OnPropertyChanged(nameof(BuildingList));
        }
    }
    public ICommand GetBuildingsCommand { get; protected set; }
    public ICommand AddBuildingCommand { get; protected set; }
    public ICommand UpdateBuildingCommand { get; protected set; }
    public ICommand BuildingSettingsCommand { get; protected set; }
    public ICommand OpenBuildingSettingsPopupCommand { get; protected set; }
    public ICommand OpenAddBuildingPopupCommand { get; protected set; }
    public ICommand ClosePopupCommand { get; protected set; }

    public ProfilePageViewModel(BuildingService buildingService)
    {
        this.buildingService = buildingService;

        AddBuildingCommand = new Command(() => AddBuildingAsync(NewBuilding));
        GetBuildingsCommand = new Command(() => GetBuildingsAsync());
        UpdateBuildingCommand = new Command(() => UpdateBuildingAsync(NewBuilding));
        OpenAddBuildingPopupCommand = new Command(async () => await MopupService.Instance.PushAsync(new AddBuildingPopup(buildingService)));
        OpenBuildingSettingsPopupCommand = new Command(async () => await MopupService.Instance.PushAsync(new BuildingSettingsPopup(buildingService)));
        ClosePopupCommand = new Command(() => ClosePopup());

        async void AddBuildingAsync(Building building)
        {
            await buildingService.AddBuilding(building);
            NewBuilding = new();
            ClosePopup();
        }

        async void GetBuildingsAsync()
        {
            if (IsBusy | IsRefreshing) return;
            try
            {
                IsBusy = true;
                IsRefreshing = true;
                ObservableCollection<Building> tempList = await buildingService.GetBuildings();
                Debug.WriteLine($"There are: {tempList.Count} buildings currently saved in database.");

                if (tempList is not null)
                {

                    foreach (var item in tempList)
                    {
                        if (BuildingList.Any(x => x.BuildingID == item.BuildingID))
                            Debug.WriteLine($"Item {item.BuildingName} already exists in BuildingList.");
                        else
                            BuildingList.Add(item);
                    }
                    Debug.WriteLine("Transferred itemList to ItemManifest");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something went wrong: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Transfer Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
        async void UpdateBuildingAsync(Building building)
        {
            await buildingService.UpdateBuilding(building);
        }

        async void ClosePopup() { await MopupService.Instance.PopAsync(); }
    }
}
