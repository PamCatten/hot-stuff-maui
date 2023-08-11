using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;
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
    public ISeries[] Series { get; set; } =
    {
        new ColumnSeries<int>
        {
            Values = new[] { 2, 3, 5, 7, 3, 4, 6, 5 },
            Stroke = null,
            Name = "test",
            Fill = new SolidColorPaint(SKColor.Parse("FC5D52")),
            MaxBarWidth = double.MaxValue,
            IgnoresBarPosition = true
        },
    };
    public IEnumerable<ISeries> PieSeries { get; set; } =
    new[] { 2, 4, 1, 4, 3 }.AsPieSeries((value, series) =>
    {
        series.InnerRadius = 0;
    });

    public MainPageViewModel()
    {
        if (ActiveBuilding is not null)
        {
            Debug.WriteLine("ActiveBuilding is not Null");
            ActiveBuilding = new Building
            {
                BuildingID = 01,
                BuildingName = "Patten House",
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
                    new Item { ItemID = 49, ItemName = "Test Item 49", ItemQuantity = 1, Room = ItemRoom.Bathroom, ItemPrice = 400.00m, Category = ItemCategory.Electronics, ItemDescription = "Test Item", Color = ItemColor.Black, BrandManufacturer = "Test Company" },
                },
                BuildingValue = 1.00m,
                BuildingItemCount = 0,
                BuildingRoomValue = new Dictionary<ItemRoom, decimal> { { ItemRoom.Attic, 0.00m }, { ItemRoom.Studio, 1.00m } },

            };
            Debug.WriteLine($"BuildingRoomValue = {ActiveBuilding.BuildingRoomValue}");
            Debug.WriteLine($"ID: {ActiveBuilding.BuildingID} NAME: {ActiveBuilding.BuildingName} DESCRIPTION: {ActiveBuilding.BuildingDescription}");
            Debug.WriteLine($"Building value: ${ActiveBuilding.BuildingValue}");
            
        }
        else
        {
            Debug.WriteLine("ActiveBuilding is Null.");
        };



    }
}
