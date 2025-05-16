using BackendASP.Data;
using BackendASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendASP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomPlannerController : ControllerBase
{
    IRoomRepository roomRepo;
    IItemRepository itemRepo;
    public RoomPlannerController(IRoomRepository roomRepo, IItemRepository itemRepo)
    {
        this.roomRepo = roomRepo;
        this.itemRepo = itemRepo;
    }
    
    [HttpGet("generate")]
    public IActionResult GenerateRoomPlan()
    {
        var roomData = roomRepo.Read(1);
        if (roomData == null)
        {
            return NotFound("Nincs elérhető szoba adat.");
        }

        var items = itemRepo.Read().ToList();

        Room room = new Room { Width = roomData.Width, Height = roomData.Height };
        RoomPlanner planner = new RoomPlanner(room);
        List<Coordinates> coordinates = new List<Coordinates>();

        foreach (var item in items)
        {
            var position = planner.TryPlaceFurniture(item);
            if (position != null)
            {
                coordinates.Add(position);
            }
        }

        return Ok(coordinates);
    }
}