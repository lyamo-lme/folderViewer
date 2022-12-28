using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("root")]
public class FolderController : Controller
{

    public FolderController()
    {
    }

    [Route("folder")]
    public async Task<ActionResult<List<Folder>>> GetFolder(int folderId)
    {
        return View("Index", new List<Folder>());
    }
}