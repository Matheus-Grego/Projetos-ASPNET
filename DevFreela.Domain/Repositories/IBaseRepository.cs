namespace DevFreela.Domain.Repositories;

public interface IBaseRepository<T>
{
    Task<List<T> >GetAll(string search = "", int? page = 1, int? pageSize = 10);
    Task<T?> GetById(Guid id);
    Task<Guid> Add(T entity);
    Task Update(T entity);
    Task<bool> Exists(Guid id);
}