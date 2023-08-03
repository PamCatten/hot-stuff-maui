using HotStuff.Services;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;

namespace HotStuff.ViewModel;

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
    private ObservableCollection<Item> itemManifest = new();
    public ObservableCollection<Item> ItemManifest
    {
        get 
        { 
            return itemManifest; 
        }
        set
        {
            itemManifest = value;
            OnPropertyChanged(nameof(ItemManifest));
        }
    }
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
            GetItemsAsync();
        });

        AppearingCommand = new Command(async () =>
        {
            Debug.WriteLine("AppearingCommand run.");
            AppearingAsync();
        });

        async void AppearingAsync()
        {
            Debug.WriteLine("Appearing() start.");
            try
            {
                GetItemsAsync();
                Debug.WriteLine("Finished getting items");
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

            //ObservableCollection<Item> ItemManifest = new();
            //if (ItemManifest is not null)
            //{
                //if (ItemManifest.Count != 0)
                    //ItemManifest.Clear();
            //}

            try
            {
                IsBusy = true;
                ObservableCollection<Item> itemList = await itemService.GetItems();
                Debug.WriteLine($"There are: {itemList.Count} items currently saved in database.");

                if (itemList is not null)
                {

                    foreach (var item in itemList)
                    {
                        if (ItemManifest.Any(x => x.ItemID == item.ItemID))
                            Debug.WriteLine($"Item {item.ItemName} already exists in ItemManifest.");
                        else
                            ItemManifest.Add(item);
                    }
                    Debug.WriteLine("Transferred itemList to ItemManifest");
                }
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
