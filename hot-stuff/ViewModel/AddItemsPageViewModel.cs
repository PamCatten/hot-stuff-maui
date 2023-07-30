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
                    $"Quantity: {NewItem.Quantity}, " +
                    $"AmountPaid: {NewItem.AmountPaid}" +
                    $"Color: {NewItem.Color}" +
                    $"Brand: {NewItem.BrandManufacturer}" +
                    $"Date: {NewItem.PurchaseDate}" +
                    $"Description: {NewItem.ItemDescription}");
                await App.ItemServ.AddItem(NewItem);
                await Shell.Current.GoToAsync("..");
            }

            AddItemCommand = new Command(async () =>
            {
                Debug.WriteLine("----User called AddItemCommand.");
                CreateItem(NewItem);
                NewItem = new();
            });
        }
    }
}
