using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Db.Repository;

public class FolderRepository : IFolderRepository
{
    private DBContext DbContext { get; set; }

    public FolderRepository(DBContext context)
    {
        DbContext = context;
    }

    public async Task<Folder> GetFolderAsync(Func<Folder, bool> funct)
    {
        try
        {
            await using (DbContext)
            {
                var model = await DbContext.Folders.FirstAsync(x => funct(x));
                model.Folders = await DbContext.Folders.Where(x => x.ParentId == model.Id).ToListAsync();
                return model;
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Folder> AddFolderAsync(Folder folder)
    {
        await using (DbContext)
        {
            try
            {
                await DbContext.Folders.AddAsync(folder);
                await DbContext.SaveChangesAsync();
                return folder;
            }
            catch
            {
                throw new Exception("error to create folder");
            }
        }
    }
}