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
    
    // Ilyenkor lehet több hibaágat kezelni pl.: NotFound, Ok (http:200)
    // public IActionResult GenerateRoomPlan()
    // return NotFound("Nincs elérhető szoba adat.");
    // return Ok(coordinates);

    [HttpGet("generate")]
    public List<Coordinates> GenerateRoomPlan()
    {
        var roomData = roomRepo.Read(1) ?? throw new Exception("Nincs elérhető szoba adat."); // 500 Internal Server Error;
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

        return coordinates;
    }
}