using BackendASP.Models;

namespace BackendASP.Services;

public interface IRoomPlannerService
{
    List<Coordinates> GeneratePlan(int roomId);
}