using Adidy.Exceptions;
using Adidy.Models;
using Adidy.Services.Interface;
using Adidy.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modele;
using Newtonsoft.Json;
using Rotativa.AspNetCore;
using System.Diagnostics;

namespace Adidy.Controllers
{
    public class HomeController(IMpandrayService MpandrayService, 
        IPaiementAdidyService paiementAdidyService, 
        IHttpContextAccessor httpContextAccessor, 
        IUtilisateurService utilisateurService,
        IPaiementIsantaonaService paiementIsantaonaService,
        IDroitUtilisateurService droitUtilisateurService) : Controller
    {
        private readonly IMpandrayService MpandrayService = MpandrayService;
        private readonly IPaiementAdidyService paiementAdidyService = paiementAdidyService;
        private readonly IUtilisateurService utilisateurService = utilisateurService;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
        private readonly IPaiementIsantaonaService paiementIsantaonaService = paiementIsantaonaService;
        private readonly IDroitUtilisateurService droitUtilisateurService = droitUtilisateurService;
        private string name = "/Home";

        public async Task<IActionResult> DataToPdf()
        {
            return await Task.Run(() =>
            {
                var t = new ViewAsPdf("Index");
                t.ViewName = "Index";
                return t;
            });
        }

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
                httpContextAccessor.HttpContext!.Session.SetString("user", JsonConvert.SerializeObject(loger));
                return RedirectToAction("MpandrayListe", "Home", new { page = 1 });
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
        public async Task<IActionResult> Search(int page, string tosearch)
        {
			string pageName = name + "/Search";
			try
			{
				await Autorisation(pageName);
			}
			catch
			{
				return RedirectToAction("Index", "Home");
			}
			if (page == 0)
            {
                page = 1;
            }
            IEnumerable<Mpandray> resultat = await MpandrayService.Search(page, tosearch);
            ViewData["liste"] = resultat;
            ViewData["page"] = page;
            string currentLink = Request.Path.ToString();
            ViewData["link"] = currentLink;

            ViewData["query"] = tosearch;
            return View("MpandrayListe");
        }

        [HttpGet("/Home/MpandrayListe/{page}")]
        public async Task<IActionResult> MpandrayListe([FromRoute]int page)
        {
            string pageName = name + "/Liste";
            try
            {
                await Autorisation(pageName);
            }
            catch
            {
                return RedirectToAction("Index","Home");
            }
            IEnumerable<Mpandray>? liste_mpandray = [];

            if (page <= 0)
            {
                page = 1;
            }
            liste_mpandray = await MpandrayService.MpandraysPaginate(page);
            ViewData["page"] = page;
            ViewData["liste"] = liste_mpandray;
            string currentLink = Request.Path.ToString();
            ViewData["link"] = currentLink;
            return View();
        }


        [HttpGet("/Home/Details/{numero}")]
        public async Task<IActionResult> MpandrayDetails([FromRoute]int numero)
        {

			string pageName = name + "/Details";
			try
			{
				await Autorisation(pageName);
			}
			catch
			{
				return RedirectToAction("Index", "Home");
			}
            Mpandray? details = await MpandrayService.GetMpandrayByNumero(numero);
            ViewData["details"] = details;
            return View();
        }

        //Modification mpandray
        [HttpPost("/Home/Details")]
        public async Task<IActionResult> MpandrayDetails(Mpandray mpandray)
        {
            string pageName = name + "/Modification";
			try
			{
				await Autorisation(pageName);
			}
			catch
			{
				return RedirectToAction("Details", "Home",new {numero = mpandray.Numero});
			}
			Mpandray? changed = await MpandrayService.UpdateMpandray(mpandray);
            IEnumerable<PaiementAdidy> listePaiements = await paiementAdidyService.GetPaiementByNumeroMpandray(mpandray.Numero);
            ViewData["details"] = changed;
            ViewData["listePaiement"] = listePaiements;
            return View();
        }

        [HttpGet("/Home/Ajout")]
        public async Task<IActionResult> MpandrayAjout()
        {
            string pageName = name + "Ajout";
			try
			{
				await Autorisation(pageName);
			}
			catch
			{
				return RedirectToAction("Details", "Home");
			}
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


        [HttpPost("/Home/AjoutPaiement")]
        public async Task<IActionResult> Paiement(string numerompandray,int type,int moisdebut,int anneedebut,int moisfin,int anneefin,string montant)
        {
            string pageName = name + "/AjoutPaiement";
            int numero = Int32.Parse(numerompandray);
			try
			{
				await Autorisation(pageName);
			}
			catch
			{
				return Redirect($"/Home/Details/{numero}"); ;
			}
			if (Constante.type_adidy[type].Equals("Adidy", StringComparison.CurrentCultureIgnoreCase))
            {
                try
                {
                    PaiementAdidy aPayer = new()
                    {
                        NumeroMpandray = Int32.Parse(numerompandray),
                        MoisDebut = moisdebut,
                        AnneeDebut = anneedebut,
                        MoisFin = moisfin,
                        AnneeFin = anneefin,
                        Montant = decimal.Parse(montant),
                        Duree = Duration.CalculDuration(moisdebut, anneedebut,moisfin,anneefin)
                    };
                    await paiementAdidyService.AddPaiement(aPayer);
                }
                catch
                {
                    ViewData["error"] = "Chiffre no atao eo amin'ilay montant";
                }
            }
            else if(Constante.type_adidy[type].Equals("ikt", StringComparison.CurrentCultureIgnoreCase))
            {
                try
                {
                    PaiementIsantaona aPayer = new()
                    {
                        NumeroMpandray = Int32.Parse(numerompandray),
                        AnneeDebut = anneedebut,
                        AnneeFin = anneefin,
                        Montant = decimal.Parse(montant)
                    };
                    await paiementIsantaonaService.AddPaiement(aPayer);
                }
                catch
                {
                    ViewData["error"] = "Chiffre no atao eo amin'ilay montant";
                }
            }
            return Redirect($"/Home/Details/{numero}");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task Autorisation(string pageName)
        {
			Utilisateur? user = null;
			if (httpContextAccessor.HttpContext!.Session.GetString("user")! == null)
			{
				TempData["error"] = "Veuillez vous connecter";
                throw new Exception();
			}
			else
			{
				user = JsonConvert.DeserializeObject<Utilisateur>(httpContextAccessor.HttpContext!.Session.GetString("user")!)!;
				if (user == null)
				{
					TempData["error"] = "Veuillez vous connecter";
					throw new Exception();
				}
			}
			bool checkDroit = await droitUtilisateurService.CheckDroit(user!.Idutilisateur, pageName);
			if (!checkDroit)
			{
				TempData["error"] = "Vous n'avez pas les droits requis. Consultez un administrateur.";
				throw new Exception();
			}
		}
    }
}
