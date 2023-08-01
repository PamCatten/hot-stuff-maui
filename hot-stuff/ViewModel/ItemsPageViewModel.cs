using HotStuff.Services;
using System.Windows.Input;
using UraniumUI;

namespace HotStuff.ViewModel;

[QueryProperty(nameof(Item), "Item")]
public partial class ItemsPageViewModel : BaseViewModel 
{
    ItemService itemService;
    public List<Item> SelectedItems { get; set; } = new List<Item>();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    public bool IsNotBusy => !IsBusy;

    public ICommand GetItemsCommand { get; protected set; }
    public ICommand AppearingCommand { get; set; }
    public ICommand RemoveSelectedItemsCommand { get; protected set; }

    private ObservableCollection<Item> itemManifest;
    public ObservableCollection<Item> ItemManifest { get { return itemManifest; } set { itemManifest = value; OnPropertyChanged(); }}

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
            GetItemsAsync();
        });

        AppearingCommand = new Command(async () =>
        {
            Debug.WriteLine("AppearingCommand run.");
            Appearing();
        });

        async void Appearing()
        {
            Debug.WriteLine("Appearing() start.");
            try
            {
                GetItemsAsync();
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


        async void GetItemsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                ObservableCollection<Item> itemList = await itemService.GetItems();
                Debug.WriteLine($"Items saved in database: {itemList.Count}");

                if (ItemManifest.Count != 0)
                    ItemManifest.Clear();

                ItemManifest = new(itemList);
                Debug.WriteLine("Transferred itemList to ItemManifest");

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

            await App.ItemService.DeleteItems(Items);
        }

        async void DeleteAllAsync()
        {
            await App.ItemService.FlushItems();
        }

    }
}


;
