using BackendASP.Data;
using BackendASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendASP.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
    IRepositoryCRUD<Room> repo;
    public RoomController(IRepositoryCRUD<Room> repo)
    {
        this.repo = repo;
    }

    [HttpGet("{id}")]
    public Room? GetRoom(int id)
    {
        return this.repo.Read(id);
    }

    [HttpPost]
    public void CreateRoom([FromBody] Room room)
    {
        this.repo.Create(room);
    }

    [HttpPut]
    public void EditRoom([FromBody] Room room)
    {
        this.repo.Update(room);
    }

    [HttpDelete("{id}")]
    public void DeleteRoom(int id)
    {
        this.repo.Delete(id);
    }
}