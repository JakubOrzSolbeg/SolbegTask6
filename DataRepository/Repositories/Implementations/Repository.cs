using DataRepository.DbContext;
using DataRepository.Entities.Base;
using DataRepository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataRepository.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly MainDbContext1 MainDbContext;

    public Repository(MainDbContext1 mainDbContext)
    {
        MainDbContext = mainDbContext;
    }

    public async Task<List<T>> GetAll()
    {
        return await MainDbContext.Set<T>().ToListAsync();
    }

    public async Task<List<T>> GetRange(List<int> ids)
    {
        var idSet = ids.ToHashSet();
        return await MainDbContext.Set<T>()
            .Where(item => idSet.Contains(item.Id))
            .ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await MainDbContext.Set<T>()
            .FirstOrDefaultAsync(o => o.Id.Equals(id));
    }

    public async Task<T> Add(T obj)
    {
        MainDbContext.Set<T>()
            .Add(obj);
        await MainDbContext.SaveChangesAsync();
        return obj;
    }

    public async Task<T> Update(T obj)
    {
        MainDbContext.Set<T>()
            .Update(obj);
        await MainDbContext.SaveChangesAsync();
        return obj;
    }

    public async Task<bool> Delete(T obj)
    {
        MainDbContext.Set<T>().Remove(obj);
        await MainDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<int> Count()
    {
        return await MainDbContext.Set<T>().CountAsync();
    }

    public async Task<bool> CheckIfExists(Predicate<T> predicate)
    { 
        return await MainDbContext.Set<T>().AnyAsync(item => predicate(item));
    }

    public async Task<T?> GetByPredicate(Predicate<T> predicate)
    {
        T? result = null;
        await Task.Run(() =>
        {
            result = MainDbContext.Set<T>()
                .AsEnumerable()
                .FirstOrDefault(item => predicate(item));
        });
        return result;
    }

    public async Task<List<T>> GetAllByPredicate(Predicate<T> predicate)
    {
        List<T> result = new List<T>();
        await Task.Run(() =>
        {
            result = MainDbContext.Set<T>()
                .AsEnumerable()
                .Where(obj => predicate(obj))
                .ToList();
        });
        return result;
    }

    public async Task Commit()
    {
        await MainDbContext.SaveChangesAsync();
    }
}