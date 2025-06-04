using BackendASP.Models;

namespace BackendASP.Data;

public interface IRepositoryCRUD<T>
{
    void Create(T item);
    T Read(int id);
    void Update(T item);
    void Delete(int id);
}