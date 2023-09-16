using SQLite;
namespace HotStuff.Services;
public class ItemService : IItemService
{
    private readonly string databasePath;
    public string StatusMessage { get; set; }
    private SQLiteAsyncConnection Database;

    async Task Init()
    {
        if (Database is not null) return;
        Database = new SQLiteAsyncConnection(databasePath);
        await Database.CreateTableAsync<Item>();
    }

    public ItemService(string DatabasePath)
    {
        databasePath = DatabasePath;
    }

    public async Task AddItem(Item item)
    {
        try
        {
            await Init();
            await Database.InsertAsync(item);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to add item {item.ItemID}, {item.ItemName}. Error: {ex.Message}");
        }
    }

    public async Task<ObservableCollection<Item>> GetItems()
    {
        try 
        {
            await Init();
            return new ObservableCollection<Item>(await Database.Table<Item>().ToListAsync());
        }
        catch (Exception ex) 
        {
            Debug.WriteLine($"Failed to get item data. Error: {ex.Message}");
            return new ObservableCollection<Item>();
        }
    }

    public async Task<ObservableCollection<Item>> RefreshItems(int buildingID)
    {
        try
        {
            await Init();
            return new ObservableCollection<Item>(await Database.Table<Item>().Where(i => i.BuildingID == buildingID).ToListAsync());
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to refresh item data. Error: {ex.Message}");
            return new ObservableCollection<Item>();
        }
    }
    public async Task DeleteItems(ObservableCollection<Item> items)
    {
        try
        {
            await Init();
            foreach (var item in items) 
                await Database.DeleteAsync<Item>(item.ItemID);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to delete item data. Error: {ex.Message}");
        }
    }
    public async Task ModifyItem(Item item)
    {
        try
        {
            await Init();
            await Database.UpdateAsync(item);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to modify item {item.ItemID}, {item.ItemName} data. Error: {ex.Message}");
        }
    }
    public async Task CopyItem(ObservableCollection<Item> items)
    {
        try
        {
            await Init();
            await Database.InsertAsync(items);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to copy item data. Error: {ex.Message}");
        }
    }

    public async Task FlushItems()
    {
        await Init();
        await Database.DeleteAllAsync<Item>();
    }
}
