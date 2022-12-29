using Microsoft.AspNetCore.Mvc;
using WebApplication1.Db.Repository;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class FolderController : Controller
{
    private IFolderRepository FolderRepository { get; set; }

    public FolderController(IFolderRepository folderRepository)
    {
        FolderRepository = folderRepository;
    }

    public async Task<ActionResult<Folder>> GetFolder(int? folderId = null)
    {
        Folder folders = new Folder();
        if (folderId == null)
            folders = await FolderRepository.GetFolderByIdAsync(folderId, true);
        else
            folders = await FolderRepository.GetFolderByIdAsync(folderId);

        return View("Index", folders);
    }
}