using CommunityToolkit.Mvvm.Messaging;
using CsvHelper;
using HotStuff.Services;
using Mopups.Services;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.Globalization;
using System.Windows.Input;

namespace HotStuff.ViewModel;
public partial class ItemsPageViewModel : BaseViewModel 
{
    readonly ItemService itemService;
    readonly BuildingService buildingService;
    public ObservableCollection<Item> Items { get; set; }
    public ObservableCollection<Item> SelectedItems { get; set; } = new ObservableCollection<Item>();
    private Item newItem = new();
    public Item NewItem { get => newItem; set { newItem = value; OnPropertyChanged(); } }
    private Item modifyItem = new();
    public Item ModifyItem { get => modifyItem; set { modifyItem = value; OnPropertyChanged(); } }
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    [ObservableProperty]
    bool isRefreshing;
    public bool IsNotBusy => !IsBusy;
    public ICommand AddItemCommand { get; protected set; }
    public ICommand OpenAddItemPopupCommand { get; protected set; }
    public ICommand OpenDeletePopupCommand { get; protected set; }
    public ICommand DeleteItemCommand { get; protected set; }
    public ICommand ClosePopupCommand { get; protected set; }
    public ICommand GetItemsCommand { get; protected set; }
    public ICommand DownloadItemCommand { get; protected set; }
    public ICommand OpenDownloadItemPopupCommand { get; protected set; }
    public ICommand OpenModifyItemPopupCommand { get; protected set; }
    public ICommand ModifyItemCommand { get; protected set; }
    public ICommand OpenTransferItemPopupCommand { get; protected set; }
    public ICommand TransferItemCommand { get; protected set; }
    public ICommand OpenCopyItemPopupCommand { get; protected set; }
    public ICommand CopyItemCommand { get; protected set; }
    public ICommand OpenProfilePopupCommand { get; protected set; }
    public ICommand TakePhotoCommand { get; protected set; }
    public ICommand PickPhotoCommand { get; protected set; }
    private ObservableCollection<Item> itemManifest = new();
    public Building ActiveBuilding { get; set; }
    public Building TransferBuilding { get; set; }
    public ObservableCollection<Item> ItemManifest
    {
        get { return itemManifest; }
        set
        {
            itemManifest = value;
            OnPropertyChanged(nameof(ItemManifest));
        }
    }
    public ItemsPageViewModel(ItemService itemService)
    {
        this.itemService = itemService;
        ModifyItem = new Item();
        //Items = new ObservableCollection<Item>(itemService.GetItems());
        GetItemsAsync();

        AddItemCommand = new Command(() => CreateItem(NewItem));
        DeleteItemCommand = new Command(() => DeleteAsync(SelectedItems));
        GetItemsCommand = new Command(() => GetItemsAsync());
        ModifyItemCommand = new Command(() => ModifyItemAsync(SelectedItems[0]));
        ClosePopupCommand = new Command(() => ClosePopup());
        TransferItemCommand = new Command(() => TransferAsync(SelectedItems));
        DownloadItemCommand = new Command(() => DownloadItemsAsync(ItemManifest));
        
        OpenAddItemPopupCommand = new Command(async () => await MopupService.Instance.PushAsync(new AddItemPopup(itemService)));
        OpenProfilePopupCommand = new Command(async () => await MopupService.Instance.PushAsync(new ProfilePopup(buildingService)));
        OpenModifyItemPopupCommand = new Command(async () =>
        {
            if (SelectedItems.Count == 1)
            {
                ModifyItem = SelectedItems[0];
                await MopupService.Instance.PushAsync(new ModifyItemPopup(itemService));
            }
            else if (SelectedItems.Count == 0)
                await Application.Current.MainPage.DisplayAlert("Selection Error", "No item selected. Please select the item you wish to modify.", "OK");
            else
                await Application.Current.MainPage.DisplayAlert("Selection Error", "Too many items selected. Please select only one item to modify.", "OK");
        });
        OpenDeletePopupCommand = new Command(async () =>
        {
            if (SelectedItems.Count > 0)
                await MopupService.Instance.PushAsync(new DeletePopup(itemService));
            else
                await Application.Current.MainPage.DisplayAlert("Selection Error", "No items selected. Please select the items you wish to delete.", "OK");
        });
        OpenCopyItemPopupCommand = new Command(async () =>
        {
            if (SelectedItems.Count > 0)
                await MopupService.Instance.PushAsync(new CopyPopup(itemService));
            else
                await Application.Current.MainPage.DisplayAlert("Selection Error", "No items selected. Please select the items you wish to copy.", "OK");
        });
        OpenTransferItemPopupCommand = new Command(async () =>
        {
            if (SelectedItems.Count > 0)
                await MopupService.Instance.PushAsync(new TransferPopup(itemService));
            else
                await Application.Current.MainPage.DisplayAlert("Selection Error", "No items selected. Please select the items you wish to transfer.", "OK");
        });
        OpenDownloadItemPopupCommand = new Command(async () =>
        {
            if (ItemManifest.Count > 0)
                await MopupService.Instance.PushAsync(new DownloadPopup(itemService));
            else
                await Application.Current.MainPage.DisplayAlert("Transfer Error", "Empty item manifest. Please add the items you wish to download.", "OK");
        });
        TakePhotoCommand = new Command(async () =>
        {
            var options = new StoreCameraMediaOptions { CompressionQuality = 100 };
            var result = await CrossMedia.Current.TakePhotoAsync(options);
            if (result is null) return;
        });
        PickPhotoCommand = new Command(async () =>
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select Image",
                FileTypes = FilePickerFileType.Images
            });
            if (result is null) 
                return;
            NewItem.PurchaseProof = result?.FullPath;
        });

        async void CreateItem(Item NewItem) // TODO: Find a better way of bootstrapping this
        { 
            NewItem.DateAcquired = NewItem.DateAcquired.Split(" 12:00:00 AM", StringSplitOptions.RemoveEmptyEntries)[0];
            await App.ItemService.AddItem(NewItem);
            NewItem = new();
            ClosePopup();
        }

        async void RefreshItemsAsync()
        {
            if (IsBusy | IsRefreshing) return;
            try
            {
                IsBusy = true; 
                IsRefreshing = true;
                ObservableCollection<Item> itemList = await itemService.RefreshItems(ActiveBuilding.BuildingID);
                if (itemList is not null)
                {
                    // Worried about Big O here. I'm not sure how .Any() is implemented under the hood but I need to move on.
                    // TODO: Check that out.
                    foreach (var item in itemList)
                    {
                        if (ItemManifest.Any(i => i.ItemID == item.ItemID)) continue;
                        else ItemManifest.Add(item);
                    }
                }
            } 
            catch (Exception ex)
            {
                Debug.WriteLine($"Something went wrong when refreshing: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Refresh Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
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
                            continue;
                        else
                            ItemManifest.Add(item);
                    }
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
        async void DeleteAsync(ObservableCollection<Item> selectedItems)
        {
            foreach (var item in selectedItems)
                ItemManifest.Remove(item);
            await App.ItemService.DeleteItems(selectedItems);
            ClosePopup();
        }
        async void ModifyItemAsync(Item item)
        {
            await App.ItemService.ModifyItem(item);
            if (SelectedItems[0] != item) ItemManifest[ItemManifest.IndexOf(SelectedItems[0])] = item;
            ClosePopup();
        }
        // FIXME: Emulator filepath dumps out in a weird spot, not sure how to proceed
        async void DownloadItemsAsync(ObservableCollection<Item> items) 
        {
            var csvPath = Path.Combine($@"{Environment.CurrentDirectory}", $"items-{DateTime.Now.ToFileTime}.csv");
            Debug.WriteLine($"Path: {csvPath}");
            try
            {
                using (var streamWriter = new StreamWriter(csvPath))
                {
                    using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                    csvWriter.WriteRecords((System.Collections.IEnumerable)items);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something went wrong: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Transfer Error", ex.Message, "OK");
            }
        }
        async void DeleteAllAsync() { await App.ItemService.FlushItems(); }
        async void TransferAsync(ObservableCollection<Item> items)
        {
            foreach (var item in SelectedItems)
            {
                item.BuildingID = TransferBuilding.BuildingID;
                await App.ItemService.ModifyItem(item);
            }
            ClosePopup();
        }
        async void ClosePopup() { await MopupService.Instance.PopAsync(); }
    }
}