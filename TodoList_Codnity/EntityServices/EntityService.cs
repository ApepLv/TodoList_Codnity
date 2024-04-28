using Microsoft.EntityFrameworkCore;
using TodoList_Codnity.Data;
using TodoList_Codnity.Services;

public class EntityService<T> : IEntityService<T> where T : class
{
    private readonly TodoListDbContext _context;

    public EntityService(TodoListDbContext context)
    {
        _context = context;
    }

    public async Task AddItemAsync(T item)
    {
        _context.Set<T>().Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteItemAsync(int id)
    {
        var item = await GetItemByIdAsync(id);
        if (item != null)
        {
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<T>> GetAllItemsAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetItemByIdAsync(int id)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }

    public async Task UpdateItemAsync(T item)
    {
        _context.Set<T>().Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Set<T>().AnyAsync(e => EF.Property<int>(e, "Id") == id);
    }
}