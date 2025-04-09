using System.ComponentModel.DataAnnotations;

namespace BackendASP.Models;

public class Item
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}