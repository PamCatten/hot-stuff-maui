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
    public ObservableCollection<Item> Items { get; set; }
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
    public ICommand OpenDeletePopupCommand { get; protected set; }
    public ICommand DeleteSelectedItemsCommand { get; protected set; }
    public ICommand ClosePopupCommand { get; protected set; }
    public ICommand GetItemsCommand { get; protected set; }
    public ICommand ExportItemsCommand { get; protected set; }
    public ICommand OpenExportItemsPopupCommand { get; protected set; }
    public ICommand OpenModifyItemPopupCommand { get; protected set; }
    public ICommand ModifyItemCommand { get; protected set; }
    public ICommand OpenTransferItemPopupCommand { get; protected set; }
    public ICommand TransferItemCommand { get; protected set; }
    public ICommand OpenCopyItemPopupCommand { get; protected set; }
    public ICommand CopyItemCommand { get; protected set; }
    public ICommand TakePhotoCommand { get; protected set; }
    public ICommand PickPhotoCommand { get; protected set; }

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
        //Items = new ObservableCollection<Item>(itemService.GetItems());
        GetItemsAsync();
        WeakReferenceMessenger.Default.Register<Building>(this, (r, m) => ActiveBuilding = m);

        DeleteSelectedItemsCommand = new Command(() =>
        {
            DeleteAsync(SelectedItems);
        });

        GetItemsCommand = new Command(() =>
        {
            GetItemsAsync();
        });

        ModifyItemCommand = new Command(() =>
        {
            ModifyItemAsync(SelectedItems[0]);
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
            await MopupService.Instance.PushAsync(new AddItemPopup(itemService));
        });

        OpenModifyItemPopupCommand = new Command(async () =>
        {
            if (SelectedItems.Count == 1)
                await MopupService.Instance.PushAsync(new ModifyItemPopup(itemService));
            else if (SelectedItems.Count == 0)
                await Application.Current.MainPage.DisplayAlert("Selection Error", "No item selected. Please select the item you wish to modify.", "OK");
            else
                await Application.Current.MainPage.DisplayAlert("Selection Error", "Too many items selected. Please select only one item to modify.", "OK");
        });

        OpenCopyItemPopupCommand = new Command(async () =>
        {
            if (SelectedItems.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Selection Error", "No items selected.", "OK");
                return;
            }

            await MopupService.Instance.PushAsync(new CopyPopup(itemService));
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

            //var stream = await result.OpenReadAsync();
            //ItemImage.Source = ImageSource.FromStream(() => stream);

            // First implementation
            //var result = await CrossMedia.Current.PickPhotoAsync();
            //if (result is null) return;

            //ItemImage.Source = result?.Path;

            //var fileInfo = new FileInfo(result?.Path);
            //var fileLength = fileInfo.Length;
        });

        AddItemCommand = new Command(async () =>
        {
            // TODO: Find a better way of bootstrapping this
            NewItem.DateAcquired = NewItem.DateAcquired.Split(" 12:00:00 AM", StringSplitOptions.RemoveEmptyEntries)[0];
            CreateItem(NewItem);
            NewItem = new();
            await MopupService.Instance.PopAsync();
        });

        OpenTransferItemPopupCommand = new Command(async () =>
        {
            if (ItemManifest.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Transfer Error", "No items saved in manifest.", "OK");
                return;
            }
            await MopupService.Instance.PushAsync(new TransferPopup(itemService));
        });

        TransferItemCommand = new Command(async () =>
        {
            // Transfer items between buildings (modify associated buildingID's?)
        });

        async void CreateItem(Item NewItem)
        {
            await App.ItemService.AddItem(NewItem);
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

        async void DeleteAsync(ObservableCollection<Item> items)
        {
            foreach (var item in items)
            {
                ItemManifest.Remove(item);
            }
            await App.ItemService.DeleteItems(items);
            await MopupService.Instance.PopAsync();
        }

        async void ModifyItemAsync(Item item)
        {
            Debug.WriteLine("Update item called.");
            await App.ItemService.ModifyItem(item);
            await MopupService.Instance.PopAsync();
        }

        async void DeleteAllAsync()
        {
            await App.ItemService.FlushItems();
        }

        async void TransferAsync(ObservableCollection<Item> items)
        {
            Debug.WriteLine("User clicked transfer.");
        }
    }
}
