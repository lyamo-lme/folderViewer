using System.Text;
using System.Text.Json;
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

    [HttpPost]
    public async Task<ActionResult<Folder>> ImportFolder(ImportFolderModel d)
    {
        try
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(d.file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());
            }
            var obj = result.ToString();
            var folder = JsonSerializer.Deserialize<Folder>(obj);
            folder.ParentId = d.parentId;
            var model = await FolderRepository.ImportFolder(folder);
            return await GetFolderView(d.parentId);
        }
        catch
        {
            throw new Exception("file was damaged");
        }
    }

    public class ImportFolderModel
    {
        public IFormFile file { get; set; }
        public int parentId { get; set; }
    }
}