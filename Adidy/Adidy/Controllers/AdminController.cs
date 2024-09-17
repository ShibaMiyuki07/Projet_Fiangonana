using Adidy.Models;
using Adidy.Services.Interface;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modele;
using Newtonsoft.Json;

namespace Adidy.Controllers
{
    public class AdminController(IUtilisateurService utilisateur,
        IMpandrayService mpandrayService,
        IPaiementAdidyService paiementAdidyService,
        IPaiementIsantaonaService paiementIsantaonaService,
        IDroitService droitService,
        IDroitUtilisateurService droitUtilisateurService,
        IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly IUtilisateurService utilisateurService = utilisateur;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
        private readonly IMpandrayService mpandrayService = mpandrayService;
        private readonly IPaiementAdidyService paiementAdidyService = paiementAdidyService;
        private readonly IPaiementIsantaonaService paiementIsantaonaService = paiementIsantaonaService;
        private readonly IDroitService droitService = droitService;
        private readonly IDroitUtilisateurService droitUtilisateurService = droitUtilisateurService;
        const string name = "/Admin";




        public async Task<IActionResult> AjoutUtilisateur()
        {
            string pageName = name + "/AjoutUtilisateur";
            try
            {
                await Autorisation(pageName);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            return View("UtilisateurAjout");
        }

        [HttpPost("/Admin/AjoutUtilisateur")]
        public async Task<IActionResult> CreateUser(Utilisateur user)
        {
            try
            {
                await utilisateurService.CreateUtilisateur(user);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
            }
            return View("UtilisateurAjout");
        }

        [HttpGet]
        public async Task<IActionResult> ImportData()
        {
            string pageName = name+"/ImportData";
            try
            {
                await Autorisation(pageName);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            return await Task.Run(IActionResult () =>
            {
                ViewData["type"] = Constante.toImport;
                return View("Import");
            });
        }

        [HttpPost]
        public async Task<IActionResult> Import(ImportCsv data)
        {
            if (!Path.GetExtension(data.File!.FileName).Contains("csv", StringComparison.CurrentCultureIgnoreCase))
            {
                ViewData["Error"] = "Les donnees a enregistre doivent etre en csv";
                return View("Import");
            }
            if (Constante.toImport[data.DataType - 1].Item2.Equals("mpandray", StringComparison.CurrentCultureIgnoreCase))
            {
                IEnumerable<Mpandray> liste_mpandray = await new CSV<Mpandray>().ImportFromIFormFile(data.File);
                await mpandrayService.BulkInsert(liste_mpandray);
            }
            else
            {
                IEnumerable<CsvAdidy> liste_adidy = await new CSV<CsvAdidy>().ImportFromIFormFile(data.File);
                IEnumerable<PaiementAdidy> liste_paiement_adidy = await CsvAdidy.CsvToPaiement(liste_adidy);
                IEnumerable<PaiementIsantaona> liste_paiement_isantaona = await CsvAdidy.CsvToPaiementIsantaona(liste_adidy);
                ViewData["PaiementAdidy"] = liste_paiement_adidy;
                ViewData["PaiementIsantaona"] = liste_paiement_isantaona;
                ViewData["type"] = Constante.toImport;
                await paiementAdidyService.BulkInsert(liste_paiement_adidy);
                await paiementIsantaonaService.BulkInsert(liste_paiement_isantaona);
                return View();
            }
            return RedirectToAction("ImportData", "Admin");
        }

        public async Task<IActionResult> UtilisateurListe()
        {
            IEnumerable<Utilisateur> liste_utilisateur = await utilisateurService.GetAllUtilisateur();
            ViewData["liste_utilisateur"] = liste_utilisateur;
            return View();
        }


        [HttpGet("/Admin/Details/{idUtilisateur}")]
        public async Task<IActionResult> UtilisateurDetails([FromRoute]string idUtilisateur)
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
            Utilisateur? utilisateur = await utilisateurService.GetUtilisateurById(idUtilisateur);
            IEnumerable<Droit> droitNotInUtilisateur = await droitService.DroitNotInUtilisateur(utilisateur);
            ViewData["details"] = utilisateur;
            ViewData["droits"] = droitNotInUtilisateur;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AjoutDroitUtilisateur(string idUtilisateur,int idDroit)
        {
            string pageName = name+"/AjoutDroitUtilisateur";
            try
            {
                await Autorisation(pageName);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            DroitUtilisateur droitUtilisateur = new()
            {
                Idutilisateur = idUtilisateur,
                Iddroit = idDroit,
                Isvalid = true
            };
            await droitUtilisateurService.Add(droitUtilisateur);
            return Redirect($"/Admin/Details/{idUtilisateur}");
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
                TempData["error"] = "Seul les administrateurs peuve acceder a cette page";
                throw new Exception();
            }
        }
    }
}
