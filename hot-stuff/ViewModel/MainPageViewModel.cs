using HotStuff.Model;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using UraniumUI;

namespace HotStuff.ViewModel;
public class MainPageViewModel : UraniumBindableObject
{
    private Building activeBuilding = new();
    public Building ActiveBuilding { get => activeBuilding; set { activeBuilding = value; OnPropertyChanged(); } }

    public List<string> ColumnChartKeys = new();
    public ObservableCollection<ObservableValue> ColumnChartValues = new();

    public ISeries[] RoomValueBarChart { get; set; }
    public IEnumerable<ISeries> CategoryCountPieChart { get; set; }
    public List<Axis> BarChartXAxis { get; set; } 
    public List<Axis> BarChartYAxis { get; set; }

    public List<string> PieChartKeys = new();
    public ObservableCollection<ObservableValue> PieChartValues = new();
    public MainPageViewModel()
    {
        try
        {
            if (ActiveBuilding is not null)
            {
                // Test data for the ActiveBuilding
                ActiveBuilding = new Building
                {
                    BuildingID = 01,
                    BuildingName = "Test House",
                    BuildingDescription = "A 3 bedroom, 2 bathroom home with a 3 car garage and a small backyard.",
                    BuildingType = BuildingType.House,
                    BuildingManifest = new ObservableCollection<Item>
                {
                    new Item { ItemID = 43, ItemName = "Test Item 43", ItemQuantity = 1, Room = ItemRoom.Attic, ItemPrice = 191.00m, Category = ItemCategory.Antiques, ItemDescription = "Test Item", Color = ItemColor.Black, BrandManufacturer = "Test Company" },
                    new Item { ItemID = 44, ItemName = "Test Item 44", ItemQuantity = 1, Room = ItemRoom.LivingRoom, ItemPrice = 182.00m, Category = ItemCategory.Fixtures, ItemDescription = "Test Item", Color = ItemColor.Black, BrandManufacturer = "Test Company" },
                    new Item { ItemID = 45, ItemName = "Test Item 45", ItemQuantity = 1, Room = ItemRoom.LaundryRoom, ItemPrice = 12.00m, Category = ItemCategory.Furniture, ItemDescription = "Test Item", Color = ItemColor.Black, BrandManufacturer = "Test Company" },
                    new Item { ItemID = 46, ItemName = "Test Item 46", ItemQuantity = 2, Room = ItemRoom.Garage, ItemPrice = 45.99m, Category = ItemCategory.SportsEquipment, ItemDescription = "Test Item", Color = ItemColor.Black, BrandManufacturer = "Test Company" },
                    new Item { ItemID = 47, ItemName = "Test Item 47", ItemQuantity = 4, Room = ItemRoom.Entryway, ItemPrice = 76.00m, Category = ItemCategory.Lighting, ItemDescription = "Test Item", Color = ItemColor.Black, BrandManufacturer = "Test Company" },
                    new Item { ItemID = 48, ItemName = "Test Item 48", ItemQuantity = 5, Room = ItemRoom.Kitchen, ItemPrice = 33.99m, Category = ItemCategory.Lighting, ItemDescription = "Test Item", Color = ItemColor.Black, BrandManufacturer = "Test Company" },
                    new Item { ItemID = 49, ItemName = "Test Item 49", ItemQuantity = 1, Room = ItemRoom.Greenhouse, ItemPrice = 400.00m, Category = ItemCategory.Electronics, ItemDescription = "Test Item", Color = ItemColor.Black, BrandManufacturer = "Test Company" },
                },
                    BuildingValue = 1.00m,
                    BuildingItemCount = 0,
                    BuildingRoomValue = new Dictionary<ItemRoom, decimal> {},
                    BuildingCategoryCount = new Dictionary<ItemCategory, int> {},
                };
            }
            else
            {
                Debug.WriteLine("ActiveBuilding is null.");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving ActiveBuilding: {ex.Message}");
            Shell.Current.DisplayAlert("Building Record Retrieval Error", ex.Message, "OK");
        }

        // TODO: This is gross, but it's good enough for now. I'm sure there's a better way to do this.
        ActiveBuilding.BuildingRoomValue.Clear();
        ActiveBuilding.BuildingCategoryCount.Clear();
        foreach (var item in ActiveBuilding.BuildingManifest)
        {
            if (ActiveBuilding.BuildingRoomValue.ContainsKey((ItemRoom)item.Room))
                ActiveBuilding.BuildingRoomValue[(ItemRoom)item.Room] += item.ItemPrice;
            else
                ActiveBuilding.BuildingRoomValue.Add((ItemRoom)item.Room, item.ItemPrice);

            if (ActiveBuilding.BuildingCategoryCount.ContainsKey((ItemCategory)item.Category))
                ActiveBuilding.BuildingCategoryCount[(ItemCategory)item.Category] += item.ItemQuantity;
            else
                ActiveBuilding.BuildingCategoryCount.Add((ItemCategory)item.Category, item.ItemQuantity);
        }

        foreach (KeyValuePair<ItemRoom, decimal> entry in ActiveBuilding.BuildingRoomValue)
        {
            Debug.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
            ColumnChartValues.Add(new((double)entry.Value));
            ColumnChartKeys.Add(entry.Key.ToString());
        }

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

        RoomValueBarChart = new ISeries[]
        {
            new ColumnSeries<ObservableValue>
            {
                Values = ColumnChartValues,
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
            Labels = ColumnChartKeys,
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

    }
}
