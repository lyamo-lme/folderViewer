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

    public async Task<Folder> GetFolderByIdAsync(int? id, bool pId = false)
    {
        try
        {
            var model = new Folder();
            if (pId)
                model = await DbContext.Folders.FirstOrDefaultAsync(x => x.ParentId == id);
            else
                model = await DbContext.Folders.FirstOrDefaultAsync(x => x.Id == id);

            model.Folders = await DbContext.Folders.Where(x => x.ParentId == model.Id).ToListAsync();

            return model;
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

    public async Task<Folder> ExportFolder(int? folderId = null)
    {
        try
        {
            var folder = await GetFolderByIdAsync(folderId, folderId == null ? true : false);
            if(folder.Folders.Count>0)
            await FillFolder(folder.Folders, 0);
            return folder;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task FillFolder(List<Folder> folder, int i = 0)
    {
        folder[i] = await GetFolderByIdAsync(folder[i].Id);
        if (folder[i].Folders.Count > 0 && folder[i].Folders.Count > i)
        {
            await FillFolder(folder[i].Folders, 0);
        }

        i++;
        if (folder.Count > i)
        {
            await FillFolder(folder, i);
        }
    }
}