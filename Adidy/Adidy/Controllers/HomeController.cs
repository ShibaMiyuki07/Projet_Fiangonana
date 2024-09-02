using Adidy.Exceptions;
using Adidy.Models;
using Adidy.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Modele;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Adidy.Controllers
{
    public class HomeController(IMpandrayService MpandrayService,IPaiementAdidyService paiementAdidyService, IHttpContextAccessor httpContextAccessor,IUtilisateurService utilisateurService) : Controller
    {
        private readonly IMpandrayService MpandrayService = MpandrayService;
        private readonly IPaiementAdidyService paiementAdidyService = paiementAdidyService;
        private readonly IUtilisateurService utilisateurService = utilisateurService;
        private IHttpContextAccessor httpContextAccessor = httpContextAccessor;
        private static string name = "Home";

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Utilisateur user)
        {
            try
            {
                Utilisateur loger = await utilisateurService.Login(user);
                httpContextAccessor.HttpContext!.Session.SetString( "user",JsonConvert.SerializeObject(loger));
                return RedirectToAction("MpandrayListe", "Home",new { page = 1});
            }
            catch (UtilisateurNotExistException e)
            {
                ViewData["error"] = e.Message;

				return View("Index");
			}
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;

				return View("Index");
			}



        }

        [HttpGet]
        public async Task<IActionResult> Search(string tosearch)
        {
            name += "/Search";
            IEnumerable<Mpandray> resultat = await MpandrayService.Search(tosearch);
            ViewData["liste"] = resultat;
            ViewData["page"] = 0;

			return View("MpandrayListe");
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
            IEnumerable<PaiementAdidy> listePaiementsAdidy = await paiementAdidyService.GetPaiementByNumeroMpandray(numero);
            ViewData["details"] = details;
            ViewData["listePaiementAdidy"] = listePaiementsAdidy;
            return View();
        }


        //Modification mpandray
        [HttpPost("/Home/Details")]
        public async Task<IActionResult> MpandrayDetails(Mpandray mpandray)
        {
            name += "/Modification";
            Mpandray? changed = await MpandrayService.UpdateMpandray(mpandray);
            IEnumerable<PaiementAdidy> listePaiements = await paiementAdidyService.GetPaiementByNumeroMpandray(mpandray.Numero);
            ViewData["details"] = changed;
            ViewData["listePaiement"] = listePaiements;
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
            ViewData["success"] = "Enregistrer ilay mpandray";
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
