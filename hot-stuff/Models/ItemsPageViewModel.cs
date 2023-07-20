using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HotStuff.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Windows.Input;
using UraniumUI;

namespace HotStuff.Models;

public partial class ItemsPageViewModel : UraniumBindableObject
{
    public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
    public ObservableCollection<Item> SelectedItems { get; set; } = new ObservableCollection<Item>();

    private Item newItem = new();
    public Item NewItem { get => newItem; set { newItem = value; OnPropertyChanged(); } }
    public ICommand AddNewItemCommand { get; protected set; }
    public ICommand GetItemsCommand { get; protected set; }
    public ICommand RemoveSelectedItemsCommand { get; protected set; }

    public ItemsPageViewModel()
    {
        async void AddItemFromDB()
        {
            await App.ItemServ.AddItem(NewItem);
        }

        AddNewItemCommand = new Command(() =>
        {
            Debug.WriteLine("User clicked add item.");
            AddItemFromDB();
            NewItem = new();
        });

        GetItemsCommand = new Command(() =>
        {
            Debug.WriteLine($"Get items called.");
            GetItemsFromDB();
        });

        async void GetItemsFromDB()
        {
            Items = await App.ItemServ.GetItems();
        }

        RemoveSelectedItemsCommand = new Command(() =>
        {
            Debug.WriteLine("Delete items called.");
            foreach (var item in SelectedItems)
            {
                Items.Remove(item);
                DeleteItemsFromDB(item);
            }
        });

        async void DeleteItemsFromDB(Item item) 
        {
            
            await App.ItemServ.RemoveItem(item);
        }

    }
}


