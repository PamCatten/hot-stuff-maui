using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HotStuff.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using UraniumUI;

namespace HotStuff.Models;

[QueryProperty(nameof(Item), "Item")]
public partial class ItemsPageViewModel : ObservableObject
{
    private ObservableCollection<Item> itemManifest { get; set; } = new();
    public ObservableCollection<Item> ItemManifest { get => itemManifest; set { itemManifest = value; OnPropertyChanged(); } }
    ItemService itemService;
    public ObservableCollection<Item> DumpList { get; set; } = new();
    public List<Item> SelectedItems { get; set; } = new List<Item>();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    public bool IsNotBusy => !IsBusy;

    public ICommand GetItemsCommand { get; protected set; }
    public ICommand AppearingCommand { get; set; }
    public ICommand RemoveSelectedItemsCommand { get; protected set; }

    public ItemsPageViewModel(ItemService itemService)
    {

        this.itemService = itemService;

        RemoveSelectedItemsCommand = new Command(async () =>
        {
            Debug.WriteLine("User clicked delete items.");
            DeleteAsync(SelectedItems);
        });

        GetItemsCommand = new Command(async () =>
        {
            Debug.WriteLine("User clicked get items.");
            await GetItemsAsync();
        });

        AppearingCommand = new Command(async () =>
        {
            Debug.WriteLine("AppearingCommand run.");
            Appearing();
        });

        async void Appearing()
        {
            Debug.WriteLine("Appearing() start.");
            var items = await App.ItemServ.GetItems();
            Debug.WriteLine("Retrieved items.");
            
            if (ItemManifest.Count == 0)
            {
                ItemManifest.Clear();
                Debug.WriteLine("Cleared ItemList.");
            }
            try
            {
                foreach (var item in items)
                {
                    ItemManifest.Add(item);
                    Debug.WriteLine($"Added {item.ItemID}, {item.ItemName}");
                }
                Debug.WriteLine("Completed loop.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get items: {ex.Message}");
                await Shell.Current.DisplayAlert("Data Retrieval Error", ex.Message, "OK");
            }
            finally
            {
                Debug.WriteLine("Appearing() end.");
            }
        }


        async Task GetItemsAsync()
        {
            ObservableCollection<Item> DumpList = new();

            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                List<Item> itemList = await itemService.GetItems();

                // DEBUG ITEM MANIFEST
                Debug.WriteLine($"Items stored in ItemManifest: {ItemManifest.Count}");
                if (ItemManifest.Count != 0)
                    ItemManifest.Clear();
                Debug.WriteLine($"Items stored in ItemManifest after clear: {ItemManifest.Count}");

                // DEBUG DATABASE ITEMLIST
                Debug.WriteLine($"Items saved in database: {itemList.Count}");

                // ADD ITEMLIST TO DUMPLIST
                foreach (var item in itemList)
                {
                    DumpList.Add(item);
                    Debug.WriteLine($"Added {item.ItemID}, {item.ItemName} to DumpList");
                }

                ItemManifest = new(DumpList);
                Debug.WriteLine("Transferred DumpList to ItemManifest");

                foreach (var item in ItemManifest)
                    Debug.WriteLine($"Item stored in ItemManifest: {item.ItemName}, {item.Category}");
            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"Something went wrong: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Transfer Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task GetItemsAsync_2() 
        {
            Debug.WriteLine("Arrived at Test Item.");
            SelectedItems.Add(new Item { ItemID = 0, PurchaseDate="6/15/2019", AmountPaid = 10.00m, Quantity = 1, BrandManufacturer = "ACME Inc.", Category = ItemCategory.PlumbingHVAC, Color = ItemColor.Magenta, ItemDescription = "Test item description.", ItemName = "Test Item", PurchaseProof = "Test Item Purchase Proof", Room = ItemRoom.Basement });
            Debug.WriteLine("Added Test Item.");
            foreach (var item in SelectedItems)
                Debug.WriteLine($"ID: {item.ItemID}, Name: {item.ItemName}, Category: {item.Category}");
            SelectedItems.Clear();
            try
            {
                List<Item> itemList = await itemService.GetItems();

                Debug.WriteLine($"Items stored in ItemManifest: {ItemManifest.Count}");
                if (ItemManifest.Count != 0)
                    ItemManifest.Clear();
                Debug.WriteLine($"Items stored in ItemManifest after clear: {ItemManifest.Count}");
                Debug.WriteLine($"Items saved in database: {itemList.Count}");
                try
                {
                    foreach (var item in itemList)
                    {
                        Debug.WriteLine($"ID: {item.ItemID}, Name: {item.ItemName}, Category: {item.Category}");
                        ItemManifest.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Issue adding to ItemManifest");
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something went wrong when retrieving items: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            Debug.WriteLine($"Items stored in ItemManifest: {ItemManifest.Count}");
            foreach (var item in ItemManifest)
                Debug.WriteLine($"Items stored in ItemManifest: {item.ItemName}");
            ObservableCollection<Item> DumpList = new(ItemManifest);
            foreach (var item in DumpList)
                Debug.WriteLine($"Item stored in DumpList: {item.ItemName}, {item.Category}");
        }

        async void DeleteAsync(List<Item> Items)
        {
            Debug.WriteLine("Delete items called.");

            foreach (var item in SelectedItems)
            {
                ItemManifest.Remove(item);
            }

            await App.ItemServ.DeleteItems(Items);
        }

        async void DeleteAllAsync()
        {
            await App.ItemServ.FlushItems();
        }

    }
}


