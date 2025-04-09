using BackendASP.Models;

namespace BackendASP.Data;

public class RoomPlanner
{
    Room room;
    string[,] grid;
    public RoomPlanner(Room room)
    {
        this.room = room;
        grid = new string[room.Height, room.Width];
        for (int i = 0; i < room.Height; i++)
        {
            for (int j = 0; j < room.Width; j++)
            {
                grid[i, j] = ".";
            }
        }
    }
    bool CanPlaceFurniture(int startX, int startY, Item item)
    {
        for (int i = 0; i < item.Height; i++)
        {
            for (int j = 0; j < item.Width; j++)
            {
                if (startX + i >= room.Height || startY + j >= room.Width || grid[startX + i, startY + j] != ".")
                {
                    return false;
                }
            }
        }
        return true;
    }

    public Coordinates TryPlaceFurniture(Item item)
    {
        for (int i = 0; i <= room.Height - item.Height; i++)
        {
            for (int j = 0; j <= room.Width - item.Width; j++)
            {
                if (CanPlaceFurniture(i, j, item))
                {
                    // Elhelyezzük az első megfelelő helyen
                    for (int x = 0; x < item.Height; x++)
                    {
                        for (int y = 0; y < item.Width; y++)
                        {
                            grid[i + x, j + y] = item.Name.Length > 1 ? item.Name.Substring(0, 1) : item.Name;
                        }
                    }
                    return new Coordinates(j, i); // Visszaadjuk az elhelyezés kezdeti koordinátáját
                }
            }
        }
        return null; // JS gets 'undefined' 
    }
}