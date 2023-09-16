using CommunityToolkit.Mvvm.Messaging;
using HotStuff.Model;
using HotStuff.Services;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Mopups.Services;
using SkiaSharp;
using System.Windows.Input;
using UraniumUI;

namespace HotStuff.ViewModel;
public partial class MainPageViewModel : BaseViewModel
{
    readonly BuildingService buildingService;
    public ICommand GetBuildingCommand { get; protected set; }
    public ICommand OpenProfilePopupCommand { get; protected set; }

    [ObservableProperty]
    bool isRefreshing;

    // Set & manage the active building
    private Building activeBuilding = new();
    public Building ActiveBuilding { get => activeBuilding; set { activeBuilding = value; OnPropertyChanged(); } }

   // Main page bar chart controls & data
    public List<string> BarChartKeys = new();
    public ObservableCollection<ObservableValue> BarChartValues = new();
    public ISeries[] RoomValueBarChart { get; set; }
    public List<Axis> BarChartXAxis { get; set; }
    public List<Axis> BarChartYAxis { get; set; }
    public List<string> ChartType { get; set;  }
    // Main page pie chart controls & data
    public IEnumerable<ISeries> CategoryCountPieChart { get; set; }
    public List<string> PieChartKeys = new();
    public ObservableCollection<ObservableValue> PieChartValues = new();

    public MainPageViewModel()
    {
        //OnboardAsync();
        GetBuildingAsync();

        ChartType = new List<string>
        {
            "Category", "Room", 
        };

        // Bar chart data handling
        foreach (KeyValuePair<ItemRoom, decimal> entry in ActiveBuilding.BuildingRoomValue)
        {
            Debug.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
            BarChartValues.Add(new((double)entry.Value));
            BarChartKeys.Add(entry.Key.ToString());
        }

        RoomValueBarChart = new ISeries[]
        {
            new ColumnSeries<ObservableValue>
            {
                Values = BarChartValues,
                Stroke = null,
                Name = "Room Total",
                Fill = new SolidColorPaint(SKColor.Parse("FC5D52")),
                MaxBarWidth = double.MaxValue,
                IgnoresBarPosition = true,
            }
        };
        BarChartXAxis = new List<Axis>
        {
            new Axis
            {
            Labels = BarChartKeys,
            LabelsRotation = 300,
            TextSize = 14,
            }
        };
        BarChartYAxis = new List<Axis>
        {
            new Axis
            {
                TextSize = 14,
                MinStep = 100,
            }
        };

        // Pie chart data handling

        foreach (KeyValuePair<ItemCategory, int> entry in ActiveBuilding.BuildingCategoryCount)
        {
            Debug.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
            PieChartValues.Add(new(entry.Value));
            PieChartKeys.Add(entry.Key.ToString());
        }

        //TODO: Need to figure out how to get the slices to display the Category name.
        CategoryCountPieChart = PieChartValues.AsPieSeries((value, series) =>
        {
            series.InnerRadius = 0;
            series.Name = "Items";
            series.DataLabelsFormatter = point =>
            {
                var pv = point.Coordinate.PrimaryValue;
                var sv = point.StackedValue!;

                var a = $"{pv}/{sv.Total}{Environment.NewLine}{sv.Share:P2}";
                return a;
            };
            
        });
        GetBuildingCommand = new Command(() => GetBuildingAsync());
        OpenProfilePopupCommand = new Command(async () => await MopupService.Instance.PushAsync(new ProfilePopup(buildingService)));
    }
    async void OnboardAsync()
    {
        if (ActiveBuilding is not null && ActiveBuilding.BuildingID == 0)
        {
            await MopupService.Instance.PushAsync(new OnboardPopup());
        }
    }
    async void GetBuildingAsync()
    {
        if (IsBusy | IsRefreshing) return;
        IsBusy = true;
        IsRefreshing = true;
        try
        {
            if (ActiveBuilding is not null && ActiveBuilding.BuildingID == 0)
            {
                // Sample data for the ActiveBuilding
                ActiveBuilding = new Building
                {
                    BuildingID = 01,
                    BuildingName = "Sample House",
                    BuildingDescription = "A sample house created to be a temporary placeholder.",
                    BuildingType = BuildingType.House,
                    BuildingManifest = new ObservableCollection<Item>
                {
                    new Item { ItemID = 01, ItemName = "Sample Item 1", ItemQuantity = 5, Room = ItemRoom.Attic, ItemPrice = 100.00m, Category = ItemCategory.Antiques, ItemDescription = "Sample Item 1", Color = ItemColor.Black, BrandManufacturer = "Sample Company 1" },
                    new Item { ItemID = 02, ItemName = "Sample Item 2", ItemQuantity = 2, Room = ItemRoom.Bathroom, ItemPrice = 200.00m, Category = ItemCategory.Fixtures, ItemDescription = "Sample Item 2", Color = ItemColor.Black, BrandManufacturer = "Sample Company 2" },
                    new Item { ItemID = 03, ItemName = "Sample Item 3", ItemQuantity = 1, Room = ItemRoom.Bedroom, ItemPrice = 300.00m, Category = ItemCategory.Furniture, ItemDescription = "Sample Item 3", Color = ItemColor.Black, BrandManufacturer = "Sample Company 3" },
                    new Item { ItemID = 04, ItemName = "Sample Item 4", ItemQuantity = 2, Room = ItemRoom.DiningRoom, ItemPrice = 400.00m, Category = ItemCategory.SportsEquipment, ItemDescription = "Sample Item 4", Color = ItemColor.Black, BrandManufacturer = "Sample Company 4" },
                    new Item { ItemID = 05, ItemName = "Sample Item 5", ItemQuantity = 4, Room = ItemRoom.Garage, ItemPrice = 500.00m, Category = ItemCategory.Lighting, ItemDescription = "Sample Item 5", Color = ItemColor.Black, BrandManufacturer = "Sample Company 5" },
                    new Item { ItemID = 06, ItemName = "Sample Item 6", ItemQuantity = 3, Room = ItemRoom.Kitchen, ItemPrice = 600.00m, Category = ItemCategory.Lighting, ItemDescription = "Sample Item 6", Color = ItemColor.Black, BrandManufacturer = "Sample Company 6" },
                    new Item { ItemID = 07, ItemName = "Sample Item 7", ItemQuantity = 1, Room = ItemRoom.LivingRoom, ItemPrice = 700.00m, Category = ItemCategory.Electronics, ItemDescription = "Sample Item 7", Color = ItemColor.Black, BrandManufacturer = "Sample Company 7" },
                },
                    BuildingValue = 1.00m,
                    BuildingItemCount = 0,
                    BuildingRoomValue = new Dictionary<ItemRoom, decimal> { },
                    BuildingCategoryCount = new Dictionary<ItemCategory, int> { },
                };
            }
            else
            {
                // THROWS ERROR
                //ObservableCollection<Building> buildingList = await buildingService.GetBuildings();
                //foreach (var building in buildingList)
                //{
                    //if (building.BuildingID == ActiveBuilding.BuildingID)
                    //{
                        //ActiveBuilding = building;
                        //break;
                    //}
                    //else
                        //Debug.WriteLine($"ID: {building.BuildingID}, NAME {building.BuildingName}");
                //}
                // Set retrieved building to ActiveBuilding
            }

            // Calculate building value and item count
            ActiveBuilding.BuildingRoomValue.Clear();
            ActiveBuilding.BuildingCategoryCount.Clear();
            foreach (var item in ActiveBuilding.BuildingManifest)
            {
                if (ActiveBuilding.BuildingRoomValue.ContainsKey((ItemRoom)item.Room))
                    ActiveBuilding.BuildingRoomValue[(ItemRoom)item.Room] += item.ItemPrice * item.ItemQuantity;
                else
                    ActiveBuilding.BuildingRoomValue.Add((ItemRoom)item.Room, item.ItemPrice * item.ItemQuantity);

                if (ActiveBuilding.BuildingCategoryCount.ContainsKey((ItemCategory)item.Category))
                    ActiveBuilding.BuildingCategoryCount[(ItemCategory)item.Category] += item.ItemQuantity;
                else
                    ActiveBuilding.BuildingCategoryCount.Add((ItemCategory)item.Category, item.ItemQuantity);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Something went wrong: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Building retrieval error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }
}
