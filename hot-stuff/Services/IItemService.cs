namespace HotStuff.Services;
public interface IItemService
{
    Task<ObservableCollection<Item>> GetItems();
    Task AddItem(Item item);
    Task UpdateItem(Item item);
    Task DeleteItems(ObservableCollection<Item> items);
    Task FlushItems();
}