namespace Advoqt.TestAssignment.Mvc.Controllers
{
    using System.Security.Claims;
    using System.Web.Mvc;

    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var claimsPrincipal = (ClaimsPrincipal)User;
            var claimIsAdmin = claimsPrincipal.FindFirst("IsAdmin");
            var isAdmin = claimIsAdmin?.Value != null && bool.Parse(claimIsAdmin.Value);

            return isAdmin ? (ActionResult)RedirectToAction("Index", "Users") : View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}