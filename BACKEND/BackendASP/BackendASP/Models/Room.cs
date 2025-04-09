using System.ComponentModel.DataAnnotations;

namespace BackendASP.Models;

public class Room
{
    [Key]
    public int Id { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}