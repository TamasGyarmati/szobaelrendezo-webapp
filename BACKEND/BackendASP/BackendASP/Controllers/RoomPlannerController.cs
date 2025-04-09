using BackendASP.Data;
using BackendASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendASP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomPlannerController : ControllerBase
{
    AppDbContext context;
    public RoomPlannerController(AppDbContext context)
    {
        this.context = context;
    }

    [HttpGet("generate")]
    public IActionResult GenerateRoomPlan()
    {
        var roomData = context.Rooms.FirstOrDefault();
        if (roomData == null)
        {
            return NotFound("Nincs elérhető szoba adat.");
        }

        var items = context.Items.ToList();

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