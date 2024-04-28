using TodoList_Codnity.Models;
using TodoList_Codnity;

namespace TodoList_Codnity.Services
{
    public interface IEntityService<T> where T : class
    {
        Task AddItemAsync(T item);
        Task DeleteItemAsync(int id);
        Task<IEnumerable<T>> GetAllItemsAsync();
        Task<T> GetItemByIdAsync(int id);
        Task UpdateItemAsync(T item);
        Task<bool> ExistsAsync(int id);
    }
}