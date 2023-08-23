using System.Windows.Input;
using UraniumUI;

namespace HotStuff.ViewModel
{
    public partial class AddItemsPageViewModel : UraniumBindableObject
    {
        private Item newItem = new();
        public Item NewItem { get => newItem; set { newItem = value; OnPropertyChanged(); } }
        public ICommand AddItemCommand { get; protected set; }
        public AddItemsPageViewModel()
        {
            async void CreateItem(Item NewItem)
            {
                Debug.WriteLine("----User called CreateItem.");
                Debug.WriteLine($"----NewItem: ID {NewItem.ItemID}, " +
                    $"Name {NewItem.ItemName}, " +
                    $"Category {NewItem.Category}, " +
                    $"Room: {NewItem.Room}, " +
                    $"Quantity: {NewItem.ItemQuantity}, " +
                    $"AmountPaid: {NewItem.ItemPrice}" +
                    $"Color: {NewItem.Color}" +
                    $"Brand: {NewItem.BrandManufacturer}" +
                    $"Date: {NewItem.DateAcquired.Split(" 12:00:00 AM", StringSplitOptions.RemoveEmptyEntries)}" +
                    $"Description: {NewItem.ItemDescription}");
                await App.ItemService.AddItem(NewItem);
                await Shell.Current.GoToAsync("..");
            }

            AddItemCommand = new Command(() =>
            {
                // TODO: Find a better way of bootstrapping this
                NewItem.DateAcquired = NewItem.DateAcquired.Split(" 12:00:00 AM", StringSplitOptions.RemoveEmptyEntries)[0];
                CreateItem(NewItem);
                NewItem = new();
            });
        }
    }
}
