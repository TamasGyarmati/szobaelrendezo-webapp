using BackendASP.Models;

namespace BackendASP.Data;

public class ItemRepository : IItemRepository
{
    AppDbContext context;
    public ItemRepository(AppDbContext context)
    {
        this.context = context;
    }
    public void Create(Item item)
    {
        this.context.Items.Add(item);
        context.SaveChanges();
    }
    public IEnumerable<Item> Read()
    {
        return this.context.Items;
    }
    public Item? Read(int id)
    {
        return this.context.Items.FirstOrDefault(x => x.Id == id);
    }
    public void Update(Item item)
    {
        Item? toUpdate = this.Read(item.Id);
        
        toUpdate.Name = item.Name;
        toUpdate.Width = item.Width;
        toUpdate.Height = item.Height;

        context.SaveChanges();
    }

    public void Delete(int id)
    {
        Item? toDelete = this.Read(id);
        this.context.Items.Remove(toDelete);
        
        context.SaveChanges();
    }

    public void DeleteAll()
    {
        this.context.Items.RemoveRange(this.context.Items);

        context.SaveChanges();
    }
}