using DataRepository.Entities.Base;

namespace DataRepository.Repositories.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    public Task<List<T>> GetAll();
    public Task<List<T>> GetRange(List<int> ids);
    public Task<T?> GetById(int id);
    public Task<T> Add(T obj);
    public Task<T> Update(T obj);
    public Task<bool> Delete(T obj);
    public Task<int> Count();
    public Task<bool> CheckIfExists(Predicate<T> predicate);
    public Task<T?> GetByPredicate(Predicate<T> predicate);
    public Task<List<T>> GetAllByPredicate(Predicate<T> predicate);
    public Task Commit();
}