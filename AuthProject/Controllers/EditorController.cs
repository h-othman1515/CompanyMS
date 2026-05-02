using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthProject.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class EditorController : Controller
    {
        // Editor dashboard
        public IActionResult Index() => View();

        // Create new post/article
        public IActionResult Create() => View();

        // Edit existing post/article
        public IActionResult Edit(int id) => View();
    }
}