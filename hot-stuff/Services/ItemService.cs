using HotStuff.Models;
using HotStuff.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotStuff.Services
{
    public class ItemService : IItemService
    {
        private string databasePath;
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

                Debug.WriteLine($"Record saved. Added: {item.ItemID}, {item.ItemName}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to add {item.ItemID}, {item.ItemName}. Error: {ex.Message}");
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

        public async Task DeleteItems(Item item)
        {
            await Init();
            Debug.WriteLine($"ItemID: {item.ItemID} ItemName: {item.ItemName} prepped for removal.");
            await Database.DeleteAsync<Item>(item);
            Debug.WriteLine("Item removed.");
        }

        Task IItemService.UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
