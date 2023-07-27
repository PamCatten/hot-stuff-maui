using HotStuff.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using UraniumUI;

namespace HotStuff.Models;

[QueryProperty(nameof(Item), "Item")]
public partial class ItemsPageViewModel : UraniumBindableObject
{
    // public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
    public List<Item> Items { get; set; } = new List<Item>();
    public ObservableCollection<Item> DisplayedItems { get; } = new ObservableCollection<Item>();
    public List<Item> SelectedItems { get; set; } = new List<Item>();

    ItemService itemService;

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
            await GetItemsAsync();
        });

        async Task GetItemsAsync() 
        {
            try
            {
                var displayedItems = await itemService.GetItems();

                Debug.WriteLine($"Items stored in DisplayedItems: {DisplayedItems.Count}");
                if (DisplayedItems.Count != 0)
                    DisplayedItems.Clear();
                Debug.WriteLine($"Items saved in database: {displayedItems.Count}");
                try
                {
                    if (DisplayedItems is not null)
                    {
                        foreach (Item item in displayedItems)
                        {
                            Debug.WriteLine($"ID: {item.ItemID}, Name: {item.ItemName}");
                            DisplayedItems.Add(item);
                            Debug.WriteLine($"Added {item.ItemID}, {item.ItemName} to DisplayedItems");
                        }
                        Debug.WriteLine("Completed loop.");
                    }
                    else
                    {
                        Debug.WriteLine($"DisplayedItems is null.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Issue with DisplayedItems");
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something went wrong when retrieving items: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }

        }

        async void DeleteAsync(List<Item> Items)
        {
            Debug.WriteLine("Delete items called.");

            foreach (var item in SelectedItems)
            {
                DisplayedItems.Remove(item);
            }

            await App.ItemServ.DeleteItems(Items);
        }

        async void DeleteAllAsync()
        {
            await App.ItemServ.FlushItems();
        }

    }
}


