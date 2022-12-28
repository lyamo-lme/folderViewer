using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class DBContext:DbContext{

    public DBContext(DbContextOptions<DBContext> optionsBuilder):base(optionsBuilder)
    {
    }
    public DbSet<Folder> Folders { get; set; }
}