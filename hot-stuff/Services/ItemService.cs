using SQLite;
namespace HotStuff.Services
{
    public class ItemService : IItemService
    {
        private readonly string databasePath;
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection Database;

        async Task Init()
        {
            if (Database is not null)
                return;

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

                Debug.WriteLine($"----Record saved. Added: {item.ItemID}, {item.ItemName}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"----Failed to add {item.ItemID}, {item.ItemName}. Error: {ex.Message}");
            }
        }

        public async Task<ObservableCollection<Item>> GetItems()
        {
            try 
            {
                await Init();
                Debug.WriteLine("Data retrieval attempted.");
                return new ObservableCollection<Item>(await Database.Table<Item>().ToListAsync());
            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"Failed to retrieve data. {ex.Message}");
            }

            return new ObservableCollection<Item>();
        }

        public async Task DeleteItems(List<Item> SelectedItems)
        {
            await Init();
            foreach (var item in SelectedItems)
            {
                Debug.WriteLine($"ItemID: {item.ItemID} ItemName: {item.ItemName} prepped for removal.");
                await Database.DeleteAsync<Item>(item.ItemID);
                Debug.WriteLine("Item removed.");
            }
        }

        public async Task UpdateItem(Item item)
        {
            try
            {
                await Init();

                await Database.UpdateAsync(item);

                Debug.WriteLine($"----Record saved. Updated: {item.ItemID}, {item.ItemName}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"----Failed to update {item.ItemID}, {item.ItemName}. Error: {ex.Message}");
            }
        }

        public async Task FlushItems()
        {
            await Init();
            Debug.WriteLine("Emergency flush started.");
            await Database.DeleteAllAsync<Item>();
            Debug.WriteLine("Emergency flush finished.");
        }
    }
}
