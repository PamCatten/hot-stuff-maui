using CommunityToolkit.Mvvm.Messaging;
using CsvHelper;
using HotStuff.Services;
using Mopups.Services;
using System.Globalization;
using System.Windows.Input;

namespace HotStuff.ViewModel;
public partial class ItemsPageViewModel : BaseViewModel 
{
    readonly ItemService itemService;
    public ObservableCollection<Item> SelectedItems { get; set; } = new ObservableCollection<Item>();
    private Item newItem = new();
    public Item NewItem { get => newItem; set { newItem = value; OnPropertyChanged(); } }
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    [ObservableProperty]
    bool isRefreshing;
    public bool IsNotBusy => !IsBusy;
    public ICommand AddItemCommand { get; protected set; }
    public ICommand OpenAddItemPopupCommand { get; protected set; }
    public ICommand OpenModifyItemPopupCommand { get; protected set; }
    public ICommand OpenDeletePopupCommand { get; protected set; }
    public ICommand ClosePopupCommand { get; protected set; }
    public ICommand GetItemsCommand { get; protected set; }
    public ICommand ExportItemsCommand { get; protected set; }
    public ICommand OpenExportItemsPopupCommand { get; protected set; }
    public ICommand DeleteSelectedItemsCommand { get; protected set;}
    public ICommand UpdateItemCommand { get; protected set; }
    private ObservableCollection<Item> itemManifest = new();
    public Building ActiveBuilding { get; set; }
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
        WeakReferenceMessenger.Default.Register<Building>(this, (r, m) => ActiveBuilding = m);

        DeleteSelectedItemsCommand = new Command(() =>
        {
            DeleteAsync();
        });

        GetItemsCommand = new Command(() =>
        {
            GetItemsAsync();
        });

        OpenExportItemsPopupCommand = new Command(async () =>
        {
            if (ItemManifest.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Transfer Error", "No items saved in manifest.", "OK");
                return;
            }

            await MopupService.Instance.PushAsync(new ExportPopup(itemService));
        });

        ExportItemsCommand = new Command(async () =>
        {
            Debug.WriteLine("----User clicked export icon.");
            var csvPath = Path.Combine($@"{Environment.CurrentDirectory}", $"items-{DateTime.Now.ToFileTime}.csv");
            Debug.WriteLine($"Path: {csvPath}");
            try
            {
                using (var streamWriter = new StreamWriter(csvPath))
                {
                    using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                    var items = App.ItemService.GetItems();
                    csvWriter.WriteRecords((System.Collections.IEnumerable)items);
                }
                Debug.WriteLine("----Made CSV.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something went wrong: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Transfer Error", ex.Message, "OK");
            }

            await MopupService.Instance.PushAsync(new ExportPopup(itemService));
        });

        OpenAddItemPopupCommand = new Command(async () =>
        {
            if (IsBusy | IsRefreshing)
                return;
            try
            {
                IsBusy = true;
                IsRefreshing = true;
                await MopupService.Instance.PushAsync(new AddItemPopup(itemService));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something went wrong when opening an AddItemPopup: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Navigation Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        });

        OpenModifyItemPopupCommand = new Command(async () =>
        {
            if (SelectedItems.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Selection Error", "No items selected.", "OK");
                return;
            }
            else if (SelectedItems.Count > 1)
            {
                await Application.Current.MainPage.DisplayAlert("Selection Error", "Too many items selected. Please select only one item to modify.", "OK");
                return;
            }

            await MopupService.Instance.PushAsync(new ModifyItemPopup(itemService));
        });

        OpenDeletePopupCommand = new Command(async () =>
        {
            if (SelectedItems.Count == 0)
            { 
                await Application.Current.MainPage.DisplayAlert("Selection Error", "No items selected.", "OK");
                return;
            }

            await MopupService.Instance.PushAsync(new DeletePopup(itemService));
        });

        ClosePopupCommand = new Command(async () =>
        {
            await MopupService.Instance.PopAsync();
        });

        AddItemCommand = new Command(async () =>
        {
            // TODO: Find a better way of bootstrapping this
            NewItem.DateAcquired = NewItem.DateAcquired.Split(" 12:00:00 AM", StringSplitOptions.RemoveEmptyEntries)[0];
            CreateItem(NewItem);
            NewItem = new();
            await MopupService.Instance.PopAsync();
        });

        async void CreateItem(Item NewItem)
        {
            Debug.WriteLine("----User called CreateItem.");
            await App.ItemService.AddItem(NewItem);
            await Shell.Current.GoToAsync("..");
        }

        async void GetItemsAsync()
        {
            if (IsBusy | IsRefreshing)
                return;

            try
            {
                IsBusy = true;
                IsRefreshing = true;
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
                IsRefreshing = false;
            }
        }

        async void DeleteAsync()
        {
            foreach (var item in SelectedItems)
            {
                Debug.WriteLine($"{item.ItemName}");
                ItemManifest.Remove(item);
            }
            await App.ItemService.DeleteItems(SelectedItems);
            await MopupService.Instance.PopAsync();
        }

        async void UpdateAsync(Item item)
        {
            Debug.WriteLine("Update item called.");
            await App.ItemService.UpdateItem(item);
        }

        async void DeleteAllAsync()
        {
            await App.ItemService.FlushItems();
        }
    }
}
