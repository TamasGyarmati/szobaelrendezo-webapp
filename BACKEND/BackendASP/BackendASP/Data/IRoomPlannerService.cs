using BackendASP.Models;

namespace BackendASP.Data;

public interface IRoomPlannerService
{
    List<Coordinates> GeneratePlan(int roomId);
}