namespace HotStuff.Services;
public interface IItemService
{
    Task<ObservableCollection<Item>> GetItems();
    Task<ObservableCollection<Item>> RefreshItems(int buildingID);
    Task AddItem(Item item);
    Task ModifyItem(Item item);
    Task DeleteItems(ObservableCollection<Item> items);
    Task FlushItems();
}