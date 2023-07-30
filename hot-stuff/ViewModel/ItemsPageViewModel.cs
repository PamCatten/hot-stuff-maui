using HotStuff.Services;
using System.Windows.Input;
using UraniumUI;

namespace HotStuff.Model;

[QueryProperty(nameof(Item), "Item")]
public partial class ItemsPageViewModel : BaseViewModel
{
    private ObservableCollection<Item> itemManifest { get; set; } = new();
    public ObservableCollection<Item> ItemManifest { get => itemManifest; set { itemManifest = value; OnPropertyChanged(); } }

    public ObservableCollection<Item> DumpList { get; private set; }

    ItemService itemService;
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
        ItemManifest = new ObservableCollection<Item>();
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


