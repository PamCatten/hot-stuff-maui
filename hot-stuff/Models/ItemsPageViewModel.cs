using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HotStuff.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Windows.Input;
using UraniumUI;

namespace HotStuff.Models;

public partial class ItemsPageViewModel : UraniumBindableObject
{
    // public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
    public List<Item> Items { get; set; } = new List<Item>();
    public ObservableCollection<Item> DisplayedItems { get; set; } = new ObservableCollection<Item>();
    public List<Item> SelectedItems { get; set; } = new List<Item>();
    public ICommand GetItemsCommand { get; protected set; }
    public ICommand RemoveSelectedItemsCommand { get; protected set; }

    public ItemsPageViewModel()
    {
         async void DeleteItemsFromDB(List<Item> Items)
        {
            Debug.WriteLine("Delete items called.");

            foreach (var item in SelectedItems)
            {
                DisplayedItems.Remove(item);
            }

            await App.ItemServ.DeleteItems(Items);
        }

        RemoveSelectedItemsCommand = new Command(async () =>
        {
            Debug.WriteLine("User clicked delete items.");
            DeleteItemsFromDB(SelectedItems);
        });
    }
}


