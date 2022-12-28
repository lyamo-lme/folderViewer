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
    public async Task<ActionResult<Folder>> GetFolder(int? folderId=null)
    {
        return View("Index", new Folder());
    }
}