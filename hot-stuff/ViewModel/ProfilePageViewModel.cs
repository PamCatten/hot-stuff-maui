using HotStuff.Services;
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
                // Some concern about Big O here, but I can't imagine it being a problem for a small app like this
                // because realistically we're talking about a handful of buildings per user at most
                // TODO: When free, look into a better way to do this
                if (tempList is not null)
                {
                    foreach (var item in tempList)
                    {
                        if (BuildingList.Any(x => x.BuildingID == item.BuildingID)) continue;
                        else BuildingList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something went wrong retrieving buildings: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Retrieval Error", ex.Message, "OK");
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
