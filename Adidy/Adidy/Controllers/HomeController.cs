using Adidy.Exceptions;
using Adidy.Models;
using Adidy.Services.Interface;
using Adidy.Utils;
using Microsoft.AspNetCore.Mvc;
using Modele;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Adidy.Controllers
{
    public class HomeController(IMpandrayService MpandrayService, 
        IPaiementAdidyService paiementAdidyService, 
        IHttpContextAccessor httpContextAccessor, 
        IUtilisateurService utilisateurService,
        IPaiementIsantaonaService paiementIsantaonaService) : Controller
    {
        private readonly IMpandrayService MpandrayService = MpandrayService;
        private readonly IPaiementAdidyService paiementAdidyService = paiementAdidyService;
        private readonly IUtilisateurService utilisateurService = utilisateurService;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
        private readonly IPaiementIsantaonaService paiementIsantaonaService = paiementIsantaonaService;
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
            if (page == 0)
            {
                page = 1;
            }
            name += "/Search";
            IEnumerable<Mpandray> resultat = await MpandrayService.Search(page, tosearch);
            ViewData["liste"] = resultat;
            ViewData["page"] = page;
            string currentLink = Request.Path.ToString();
            ViewData["link"] = currentLink;

            ViewData["query"] = tosearch;
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
            string currentLink = Request.Path.ToString();
            ViewData["link"] = currentLink;
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


        [HttpPost("/Home/AjoutPaiement")]
        public async Task<IActionResult> Paiement(string numerompandray,int type,int moisdebut,int anneedebut,int moisfin,int anneefin,string montant)
        {
            name += "/AjoutPaiement";
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
            return RedirectToAction("Details", "Home", new { numero = numerompandray });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
