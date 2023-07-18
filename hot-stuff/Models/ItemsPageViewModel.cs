using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UraniumUI;

namespace HotStuff.Models;

public partial class ItemsPageViewModel : BindableObject
{
    public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
    public ObservableCollection<Item> SelectedItems { get; set; } = new ObservableCollection<Item>();

    private Item newItem = new();
    public Item NewItem { get => newItem; set { newItem = value; OnPropertyChanged(); } }

    public ItemsPageViewModel()
    {
        foreach (var item in Items)
        {
            Items.Add(new Item 
            {
                ItemName = item.ItemName,
                Category = item.Category,
                Room = item.Room,
                ItemVersion = item.ItemVersion,
                ItemDescription = item.ItemDescription,
                Color = item.Color,
                AmountPaid = item.AmountPaid,
                BrandManufacturer = item.BrandManufacturer,
                PurchaseDate = item.PurchaseDate,
                PurchaseProof = item.PurchaseProof,
            });
        }
        Items.Add(new Item
        {
            ItemName = "Pride and Prejudice",
            Category = ItemCategory.BooksMagazines,
            BrandManufacturer = "Bantam Classics",
            Room = ItemRoom.Library,
            ItemVersion = "Perma-Bound Hardcover",
            Color = ItemColor.Black,
            PurchaseDate = "09/02/2017",
            AmountPaid = 12.99m,
            ItemDescription = "Written by Jane Austen",
            PurchaseProof = "https://www.aws.com/exampleurl/"
        });

        Items.Add(new Item
        {
            ItemName = "Aug 2023 VOGUE Magazine",
            Category = ItemCategory.BooksMagazines,
            BrandManufacturer = "Vogue",
            Room = ItemRoom.Library,
            ItemVersion = "Aug 2023 Issue",
            Color = ItemColor.White,
            PurchaseDate = "07/17/2023",
            AmountPaid = 3.99m,
            ItemDescription = "August 2023 Issue, Olivia Rodrigo cover-model",
            PurchaseProof = "https://www.aws.com/exampleurl/"
        });
    }

    [RelayCommand]
    async Task AddNewItem()
    {
        Items.Insert(0, NewItem);
        NewItem = new Item();
    }

    [RelayCommand]
    async Task RemoveSelectedItem()
    {
        foreach (var item in SelectedItems)
        {
            Items.Remove(item);
        }
    }
}


