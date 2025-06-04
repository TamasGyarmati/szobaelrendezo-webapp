namespace BackendASP.Data;

public interface IRepositoryPLUS<T>
{
    void Create(T item);
    T Read(int id);
    void Update(T item);
    void Delete(int id);
    IEnumerable<T> Read();
    void DeleteAll();
}