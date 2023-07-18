using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using UraniumUI;

namespace HotStuff.Models;

public partial class Item : ObservableObject
{
    [ObservableProperty]
    string itemName;

    [ObservableProperty]
    string brandManufacturer;

    [ObservableProperty]
    string itemDescription;

    [ObservableProperty]
    string itemVersion;

    [ObservableProperty]
    string purchaseDate;

    [ObservableProperty]
    string purchaseProof;

    [ObservableProperty]
    decimal amountPaid;

    public ItemCategory Category { get; set; }

    public ItemRoom Room { get; set; }

    public ItemColor Color { get; set; }

    public static ItemCategory[] AvailableCategories => Enum.GetValues(typeof(ItemCategory)) as ItemCategory[];
    public static ItemRoom[] AvailableRooms => Enum.GetValues(typeof(ItemRoom)) as ItemRoom[];
    public static ItemColor[] AvailableColors => Enum.GetValues(typeof(ItemColor)) as ItemColor[];
}
