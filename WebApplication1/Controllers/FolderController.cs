using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using WebApplication1.Db.Repository;
using WebApplication1.Models;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace WebApplication1.Controllers;

public class FolderController : Controller
{
    private IFolderRepository FolderRepository { get; set; }

    public FolderController(IFolderRepository folderRepository)
    {
        FolderRepository = folderRepository;
    }

    public async Task<ActionResult<Folder>> GetFolderView(int? folderId = null)
    {
        return View("Index", await GetFolder(folderId));
    }

    private async Task<Folder> GetFolder(int? folderId = null)
    {
        Folder folders = new Folder();
        if (folderId == null)
            folders = await FolderRepository.GetFolderByIdAsync(folderId, true);
        else
            folders = await FolderRepository.GetFolderByIdAsync(folderId);
        return folders;
    }

    public async Task<IActionResult> ExportFolder(int? folderId = null)
    {
        var folder = await FolderRepository.ExportFolder(folderId);
        string json = JsonSerializer.Serialize(folder);
        return File(Encoding.UTF8.GetBytes(json), "text/json", folder.Name);
        
    }
}