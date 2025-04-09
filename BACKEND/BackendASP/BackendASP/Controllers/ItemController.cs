using BackendASP.Data;
using BackendASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendASP.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    IItemRepository repo;
    public ItemController(IItemRepository repo)
    {
        this.repo = repo;
    }

    [HttpGet]
    public IEnumerable<Item> GetItems()
    {
        return this.repo.Read();
    }

    [HttpGet("{id}")]
    public Item? GetItem(int id)
    {
        return this.repo.Read(id);
    }

    [HttpPost]
    public void CreateItem([FromBody] Item item)
    {
        this.repo.Create(item);
    }

    [HttpPut]
    public void EditItem([FromBody] Item item)
    {
        this.repo.Update(item);
    }

    [HttpDelete("{id}")]
    public void DeleteItem(int id)
    {
        this.repo.Delete(id);
    }

    [HttpDelete]
    public void DeleteAllItems()
    {
        this.repo.DeleteAll();
    }
}