using BackendASP.Models;

namespace BackendASP.Data;

public class RoomPlannerService : IRoomPlannerService
{
    IRoomRepository roomRepo;
    IItemRepository itemRepo;

    public RoomPlannerService(IRoomRepository roomRepo, IItemRepository itemRepo)
    {
        this.roomRepo = roomRepo;
        this.itemRepo = itemRepo;
    }

    // Ilyenkor lehet több hibaágat kezelni pl.: NotFound, Ok (http:200)
    // public IActionResult GenerateRoomPlan()
    // return NotFound("Nincs elérhető szoba adat.");
    // return Ok(coordinates);
    
    public List<Coordinates> GeneratePlan(int roomId)
    {
        var roomData = roomRepo.Read(roomId) ?? throw new Exception("Nincs elérhető szoba adat.");
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