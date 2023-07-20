using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HotStuff.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Input;
using UraniumUI;

namespace HotStuff.Models;

public partial class ItemsPageViewModel : UraniumBindableObject
{
    public ObservableCollection<Item> Items { get; set; }
    public ObservableCollection<Item> SelectedItems { get; set; } = new ObservableCollection<Item>();

    private Item newItem = new();
    public Item NewItem { get => newItem; set { newItem = value; OnPropertyChanged(); } }
    public ICommand AddNewItemCommand { get; protected set; }
    public ICommand GetCommand { get; protected set; }
    public ICommand RemoveSelectedItemsCommand { get; protected set; }

    public ItemsPageViewModel()
    {
        if (Items.Count == 0)
        {
            Items.Add(new Item
            {
                ItemName = "Pride and Prejudice",
                Category = ItemCategory.BooksMagazines,
                BrandManufacturer = "Bantam Classics",
                Room = ItemRoom.Library,
                Color = ItemColor.Black,
                PurchaseDate = "09/02/2017",
                AmountPaid = 12.99m,
                ItemDescription = "Perma-Bound Hardcover",
                PurchaseProof = "https://www.aws.com/exampleurl/"
            });

            Items.Add(new Item
            {
                ItemName = "Aug 2023 VOGUE Magazine",
                Category = ItemCategory.BooksMagazines,
                BrandManufacturer = "Vogue",
                Room = ItemRoom.Library,
                Color = ItemColor.White,
                PurchaseDate = "07/17/2023",
                AmountPaid = 3.99m,
                ItemDescription = "August 2023 Issue, Olivia Rodrigo cover-model",
                PurchaseProof = "https://www.aws.com/exampleurl/"
            });
        }

        async void AddItemToDatabase(object sender, EventArgs args)
        {
            App.ItemServ.AddItem(NewItem);
        }

        async void OnGetItems(object sender, EventArgs args)
        {
            Items = await App.ItemServ.GetItems();
        }

        AddNewItemCommand = new Command(() =>
        {
            Items.Insert(0, NewItem);
            NewItem = new();
        });

        RemoveSelectedItemsCommand = new Command(() =>
        {
            foreach (var item in SelectedItems)
            {
                Items.Remove(item);
            }
        });

    }
}


