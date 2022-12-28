using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Folder
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }
    
    [NotMapped]
    public List<Folder> Folders { get; set; }
}