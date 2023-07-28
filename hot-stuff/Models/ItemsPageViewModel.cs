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
    public ObservableCollection<Item> Items { get; } = new();
    ItemService itemService;
    public List<Item> SelectedItems { get; set; } = new List<Item>();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    public bool IsNotBusy => !IsBusy;

    public ICommand GetItemsCommand { get; protected set; }
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
            await GetItemsAsync_2();
        });

        async Task GetItemsAsync_2()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var items = await itemService.GetItemsAsync();
                if (Items.Count != 0)
                    Items.Clear();
                foreach (var item in items)
                    Items.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get items: {ex.Message}");
                await Shell.Current.DisplayAlert("Data Retrieval Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task GetItemsAsync() 
        {
            //Debug.WriteLine("Arrived at Test Item.");
            //DisplayedItems.Add(new Item { ItemID = 0, AmountPaid = 10.00m, Quantity = 1, BrandManufacturer = "ACME Inc.", Category = ItemCategory.PlumbingHVAC, Color = ItemColor.Magenta, ItemDescription = "Test item description.", ItemName = "Test Item", PurchaseProof = "Test Item Purchase Proof", Room = ItemRoom.Basement });
            //Debug.WriteLine("Added Test Item.");
            try
            {
                List<Item> displayedItems = await itemService.GetItems();

                Debug.WriteLine($"Items stored in DisplayedItems: {Items.Count}");
                if (Items.Count != 0)
                    Items.Clear();
                Debug.WriteLine($"Items saved in database: {displayedItems.Count}");
                try
                {
                    foreach (var item in displayedItems)
                    {
                        Debug.WriteLine($"ID: {item.ItemID}, Name: {item.ItemName}, Category: {item.Category}");
                        Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Issue within TempDisplay");
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something went wrong when retrieving items: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            Debug.WriteLine($"Items stored in DisplayedItems: {Items.Count}");
        }

        async void DeleteAsync(List<Item> Items)
        {
            Debug.WriteLine("Delete items called.");

            foreach (var item in SelectedItems)
            {
                Items.Remove(item);
            }

            await App.ItemServ.DeleteItems(Items);
        }

        async void DeleteAllAsync()
        {
            await App.ItemServ.FlushItems();
        }

    }
}


