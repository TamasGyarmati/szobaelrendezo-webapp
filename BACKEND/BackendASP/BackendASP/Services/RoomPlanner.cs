using BackendASP.Models;

namespace BackendASP.Services;
public class RoomPlanner
{
    int height;
    int width;
    string[,] grid;

    public RoomPlanner(int height, int width)
    {
        this.height = height;
        this.width = width;
        
        grid = new string[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
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
                if (startX + i >= height || startY + j >= width || grid[startX + i, startY + j] != ".")
                {
                    return false;
                }
            }
        }
        return true;
    }

    public Coordinates TryPlaceFurniture(Item item)
    {
        for (int i = 0; i <= height - item.Height; i++)
        {
            for (int j = 0; j <= width - item.Width; j++)
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