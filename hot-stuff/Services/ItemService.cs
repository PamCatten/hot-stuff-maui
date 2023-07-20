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
    public class ItemService
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

                Debug.WriteLine($"Record saved. Added: {item.ItemName}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to add {item.ItemName}. Error: {ex.Message}");
            }
        }

        public async Task<ObservableCollection<Item>> GetItems()
        {
            try 
            {
                await Init();
                ObservableCollection<Item> items = new(await Database.Table<Item>().ToListAsync());
                return items;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"Failed to retrieve data. {ex.Message}");
            }

            return new ObservableCollection<Item>();
        }

        public async Task RemoveItem(Item item)
        {
            await Init();
            await Database.DeleteAsync<Item>(item.ItemID);
        }

    }
}
