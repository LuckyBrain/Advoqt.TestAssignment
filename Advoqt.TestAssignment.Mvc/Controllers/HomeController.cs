namespace Advoqt.TestAssignment.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Models;
    using Newtonsoft.Json;

    [Authorize]
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var claimsPrincipal = (ClaimsPrincipal)User;
            var claimIsAdmin = claimsPrincipal.FindFirst("IsAdmin");
            var isAdmin = claimIsAdmin != null && bool.Parse(claimIsAdmin.Value);

            if (isAdmin) return RedirectToAction("Index", "Users");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13803");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("api/locations");
                if (!response.IsSuccessStatusCode) return new HttpStatusCodeResult(response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<IEnumerable<LocationModel>>(responseString);
                return View(model);
            }
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