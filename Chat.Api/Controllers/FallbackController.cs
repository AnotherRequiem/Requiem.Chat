using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

public class FallbackController : Controller
{
    // GET
    public ActionResult Index()
    {
        return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
    }
}