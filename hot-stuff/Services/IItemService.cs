using HotStuff.Models;

namespace HotStuff.Services
{
    public interface IItemService
    {
        Task<List<Item>> GetItems();
        Task AddItem(Item item);
        Task UpdateItem(Item item);
        Task DeleteItems(List<Item> item);
    }
}