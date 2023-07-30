﻿using SQLite;
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
                Debug.WriteLine("----Awaiting Init");
                await Init();

                await Database.InsertAsync(item);

                Debug.WriteLine($"----Record saved. Added: {item.ItemID}, {item.ItemName}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"----Failed to add {item.ItemID}, {item.ItemName}. Error: {ex.Message}");
            }
        }

        public async Task<List<Item>> GetItems()
        {
            try 
            {
                await Init();
                Debug.WriteLine("Data retrieval attempted.");
                return new(await Database.Table<Item>().ToListAsync());
            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"Failed to retrieve data. {ex.Message}");
            }

            return new List<Item>();
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

        Task IItemService.UpdateItem(Item item)
        {
            throw new NotImplementedException();
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
