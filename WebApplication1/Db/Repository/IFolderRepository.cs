using WebApplication1.Models;

namespace WebApplication1.Db.Repository;

public interface IFolderRepository
{
    public Task<Folder> GetFolderAsync(Func<Folder, bool> funct);
    public Task<Folder> AddFolderAsync(Folder folder);
}