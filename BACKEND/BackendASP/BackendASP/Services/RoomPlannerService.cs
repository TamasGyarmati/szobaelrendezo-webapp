using BackendASP.Data;
using BackendASP.Models;

namespace BackendASP.Services;
public class RoomPlannerService : IRoomPlannerService
{
    IRepositoryCRUD<Room> roomRepo;
    IRepositoryPLUS<Item> itemRepo;

    public RoomPlannerService(IRepositoryCRUD<Room> roomRepo, IRepositoryPLUS<Item> itemRepo)
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

        RoomPlanner planner = new RoomPlanner(roomData.Height, roomData.Width);
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