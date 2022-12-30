using WebApplication1.Models;

namespace WebApplication1.Db.Repository;

public interface IFolderRepository
{
    public Task<Folder> ImportFolder(Folder model);
    public Task<Folder> ExportFolder(int? folderId = null);
    public Task<Folder> GetFolderByIdAsync(int? id, bool pId = false);
    public Task<Folder> AddFolderAsync(Folder folder);
}