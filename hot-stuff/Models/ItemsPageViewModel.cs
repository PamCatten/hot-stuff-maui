using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UraniumUI;

namespace HotStuff.Models;

[INotifyPropertyChanged]
[QueryProperty("Item", "Item")]
public partial class ItemsPageViewModel
{
    [ObservableProperty]
    Item item;


    public ObservableCollection<Item> Items { get; protected set; } = new ObservableCollection<Item>();
    public ObservableCollection<Item> SelectedItems { get; set; } = new ObservableCollection<Item>();

    private Item newItemPrivate = new();
    public Item newItemPublic { get => newItemPrivate; set { newItemPrivate = value; OnPropertyChanged(); } }

    public ICommand AddNewItemCommand { get; set; }
    public ICommand RemoveSelectedItemCommand { get; protected set; }
    public Item Item { get => item; set => item = value; }

    [RelayCommand]
    AddNewItemCommand = new Command(() =>
    {
        Items.Insert(0, newItemPublic);
        newItemPublic = new();
    });

    [RelayCommand]
    RemoveSelectedItemCommand = new Command(() =>
    {
        foreach (var item in SelectedItems)
        {
            Items.Remove(item);
        }

    });
}


