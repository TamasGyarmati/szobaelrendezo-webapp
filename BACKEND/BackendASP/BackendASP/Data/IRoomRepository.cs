using BackendASP.Models;

namespace BackendASP.Data;

public interface IRoomRepository
{
    void Create(Room room);
    Room Read(int id);
    void Update(Room room);
    void Delete(int id);
}