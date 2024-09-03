using Adidy.Models;
using Adidy.Services.Interface;
using Data;
using Microsoft.AspNetCore.Mvc;
using Modele;

namespace Adidy.Controllers
{
    public class AdminController(IUtilisateurService utilisateur, IMpandrayService mpandrayService, IPaiementAdidyService paiementAdidyService, IPaiementIsantaonaService paiementIsantaonaService) : Controller
    {
        private readonly IUtilisateurService utilisateurService = utilisateur;
        private readonly IMpandrayService mpandrayService = mpandrayService;
        private readonly IPaiementAdidyService paiementAdidyService = paiementAdidyService;
        private readonly IPaiementIsantaonaService paiementIsantaonaService = paiementIsantaonaService;

        public IActionResult Index()
        {
            return View();
        }



        [HttpPost("/Admin/Index")]
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
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ImportData()
        {
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
            else if (Constante.toImport[data.DataType - 1].Item2.Equals("adidy", StringComparison.CurrentCultureIgnoreCase))
            {
                IEnumerable<CsvAdidy> liste_adidy = await new CSV<CsvAdidy>().ImportFromIFormFile(data.File);
                IEnumerable<PaiementAdidy> liste_paiement_adidy = await new CsvAdidy().CsvToPaiement(liste_adidy);
                IEnumerable<PaiementIsantaona> liste_paiement_isantaona = await new CsvAdidy().CsvToPaiementIsantaona(liste_adidy);
                ViewData["PaiementAdidy"] = liste_paiement_adidy;
                ViewData["PaiementIsantaona"] = liste_paiement_isantaona;
                ViewData["type"] = Constante.toImport;
                await paiementAdidyService.BulkInsert(liste_paiement_adidy);
                await paiementIsantaonaService.BulkInsert(liste_paiement_isantaona);
                return View();
            }
            return RedirectToAction("ImportData", "Admin");
        }
    }
}
