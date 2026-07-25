using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    public class UserRegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
