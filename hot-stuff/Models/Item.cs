using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.Collections.ObjectModel;

namespace HotStuff.Models;

[Table("Items")]
public partial class Item : ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int ItemID { get; set; }

    [ObservableProperty]
    string itemName;

    [ObservableProperty]
    string brandManufacturer;

    [ObservableProperty]
    string itemDescription;

    [ObservableProperty]
    string purchaseDate;

    [ObservableProperty]
    string purchaseProof;

    [ObservableProperty]
    decimal amountPaid;

    [ObservableProperty]
    int quantity;

    public ItemCategory Category { get; set; }

    public ItemRoom Room { get; set; }

    public ItemColor Color { get; set; }

    public static ItemCategory[] AvailableCategories => Enum.GetValues(typeof(ItemCategory)) as ItemCategory[];
    public static ItemRoom[] AvailableRooms => Enum.GetValues(typeof(ItemRoom)) as ItemRoom[];
    public static ItemColor[] AvailableColors => Enum.GetValues(typeof(ItemColor)) as ItemColor[];
}
