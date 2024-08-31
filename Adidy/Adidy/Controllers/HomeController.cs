using Adidy.Models;
using Adidy.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Modele;
using System.Diagnostics;

namespace Adidy.Controllers
{
    public class HomeController(IMpandrayService MpandrayService) : Controller
    {
        private readonly IMpandrayService MpandrayService = MpandrayService;
        private static string name = "Home";

        public IActionResult Index()
        {
            
            return View();
        }

        public async Task<IActionResult> Home(string tosearch)
        {
            name += "/Home";
            IEnumerable<Mpandray> resultat = await MpandrayService.Search(tosearch);
            return await Task.Run(() =>
            {
                return View();
            }
            );
        }

        [HttpGet]
        public async Task<IActionResult> MpandrayListe(int page)
        {
            IEnumerable<Mpandray>? liste_mpandray;

			if (page <= 0)
            {
                liste_mpandray = null;
			}
            else
            {
				liste_mpandray = await MpandrayService.MpandraysPaginate(page);
			}
			ViewData["liste"] = liste_mpandray;
			return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
