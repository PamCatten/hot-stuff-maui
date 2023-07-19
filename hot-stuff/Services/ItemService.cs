using HotStuff.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotStuff.Services
{
    public static class ItemService
    {
        static SQLiteAsyncConnection DB;
        static async Task Init()
        {
            if (DB != null)
                return;

            // get an absolute path to the database file
            var DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "ItemsData.db");

            var DB = new SQLiteAsyncConnection(DatabasePath);

            await DB.CreateTableAsync<Item>();

        }
        public static async Task AddItem(ItemRoom Room, ItemCategory Category, ItemColor Color)
        {
            await Init();
            var item = new Item
            {
            };
            var ItemID = await DB.InsertAsync(item);
        }
        public static async Task RemoveItem(int ItemID)
        {
            await Init();
            await DB.DeleteAsync<Item>(ItemID);
        }

        public static async Task<IEnumerable<Item>>GetItems()
        {
            await Init();

            var item = await DB.Table<Item>().ToListAsync();
            return item;
        }
    }
}
