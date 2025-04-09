using BackendASP.Models;

namespace BackendASP.Data;

public interface IItemRepository
{
    void Create(Item item);
    IEnumerable<Item> Read();
    Item? Read(int id);
    void Update(Item item);
    void Delete(int id);
    void DeleteAll();
}