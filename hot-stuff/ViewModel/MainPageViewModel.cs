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

    public ISeries[] Series { get; set; }
    public List<Axis> XAxis { get; set; } 
    public List<Axis> YAxis { get; set; }

    public Dictionary<ItemRoom, decimal> ItemDictionary = new();

    public IEnumerable<ISeries> PieSeries { get; set; } =
    new[] { 2, 4, 1, 4, 3 }.AsPieSeries((value, series) =>
    {
        series.InnerRadius = 0;

    });
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
        foreach (var item in ActiveBuilding.BuildingManifest)
        {
            if (ActiveBuilding.BuildingRoomValue.ContainsKey((ItemRoom)item.Room))
                ActiveBuilding.BuildingRoomValue[(ItemRoom)item.Room] += item.ItemPrice;
            else
                ActiveBuilding.BuildingRoomValue.Add((ItemRoom)item.Room, item.ItemPrice);
        }
        ItemDictionary = ActiveBuilding.BuildingRoomValue;
        Debug.WriteLine($"ItemDictionary: {ItemDictionary}");

        foreach (KeyValuePair<ItemRoom, decimal> entry in ActiveBuilding.BuildingRoomValue)
        {
            Debug.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
            //ColumnChartValues.Add(new ObservableValue((double)entry.Value)); C# 9
            ColumnChartValues.Add(new((double)entry.Value));
            ColumnChartKeys.Add(entry.Key.ToString());
        }

        Series = new ISeries[]
        {
            new ColumnSeries<ObservableValue>
            {
                Values = ColumnChartValues,
                Stroke = null,
                Name = ActiveBuilding.BuildingName,
                Fill = new SolidColorPaint(SKColor.Parse("FC5D52")),
                MaxBarWidth = double.MaxValue,
                IgnoresBarPosition = true
            }
        };

        XAxis = new List<Axis>
        {
            new Axis
            {
            Labels = ColumnChartKeys,
            LabelsRotation = 300,
            TextSize = 12,
            }
        };

        YAxis = new List<Axis>
        {
            new Axis
            {
                
            }
        };

    }
}
