using BackendASP.Models;
using BackendASP.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendASP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomPlannerController : ControllerBase
{
    IRoomPlannerService roomPlannerService;

    public RoomPlannerController(IRoomPlannerService roomPlannerService)
    {
        this.roomPlannerService = roomPlannerService;
    }

    [HttpGet("generate")]
    public List<Coordinates> GenerateRoomPlan()
    {
        var coordinates = roomPlannerService.GeneratePlan(1);
        return coordinates;
    }
}