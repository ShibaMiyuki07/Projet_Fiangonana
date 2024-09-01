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
            name += "/Liste";
            IEnumerable<Mpandray>? liste_mpandray;

			if (page <= 0)
            {
                liste_mpandray = [];
                page = 0;
			}
            else
            {
				liste_mpandray = await MpandrayService.MpandraysPaginate(page);
			}
            ViewData["page"] = page;
			ViewData["liste"] = liste_mpandray;
			return View();
        }


        [HttpGet("/Home/Details")]
        public async Task<IActionResult> MpandrayDetails(int numero)
        {
            name += "/Details";
            Mpandray? details = await MpandrayService.GetMpandrayByNumero(numero);
            ViewData["details"] = details;
            return View();
        }


        //Modification mpandray
        [HttpPost("/Home/Details")]
        public async Task<IActionResult> MpandrayDetails(Mpandray mpandray)
        {
            name += "/Modification";
            Mpandray? changed = await MpandrayService.UpdateMpandray(mpandray);
            ViewData["details"] = changed;
            return View();
        }

        [HttpGet("/Home/Ajout")]
        public async Task<IActionResult> MpandrayAjout()
        {
            name += "Ajout";
            return await Task.Run(() =>
                {
                    return View();
                }
            );
        }

        [HttpPost("/Home/Ajout")]
        public async Task<IActionResult> MpandrayAjout(Mpandray mpandray)
        {
            name += "Ajout";
            try
            {
                await MpandrayService.InsertOne(mpandray);
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return View();
            }
            ViewData["success"] = "";
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
