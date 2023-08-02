using HotStuff.Services;
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

            ObservableCollection<Item> ItemManifest = new();

            try
            {
                IsBusy = true;

                ObservableCollection<Item> itemList = await itemService.GetItems();
                Debug.WriteLine($"Items saved in database: {itemList.Count}");

                try
                {
                    if (itemList is not null)
                    {
                        foreach (var item in itemList)
                        {
                            ItemManifest.Add(item);
                            Debug.WriteLine($"Item stored in ItemManifest: {item.ItemName}, {item.Category}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Found the error: {ex.Message}");
                    await Application.Current.MainPage.DisplayAlert("Transform Error", ex.Message, "OK");
                }
                Debug.WriteLine("Transferred itemList to ItemManifest");
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
