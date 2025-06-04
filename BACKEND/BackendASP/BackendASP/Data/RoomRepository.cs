using BackendASP.Models;

namespace BackendASP.Data;

public class RoomRepository : IRepositoryCRUD<Room>
{
    AppDbContext context;
    public RoomRepository(AppDbContext context)
    {
        this.context = context;
    }
    public void Create(Room room)
    {
        this.context.Rooms.Add(room);
        context.SaveChanges();
    }

    // Itt ez lehetne ID nélkül is hiszen csak 1db van
    public Room Read(int id)
    {
        return this.context.Rooms.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Item> Read()
    {
        throw new NotImplementedException();
    }

    public void Update(Room room)
    {
        Room toUpdate = this.Read(room.Id);
        
        toUpdate.Height = room.Height;
        toUpdate.Width = room.Width;

        context.SaveChanges();
    }

    public void Delete(int id)
    {
        Room toDelete = this.Read(id);
        this.context.Rooms.Remove(toDelete);

        context.SaveChanges();
    }

    public void DeleteAll()
    {
        Room toDelete = this.Read(1);
        this.context.Rooms.Remove(toDelete);

        context.SaveChanges();
    }
}