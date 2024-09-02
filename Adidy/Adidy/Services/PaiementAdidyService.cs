using Adidy.Contexts;
using Adidy.Services.Interface;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Modele;
using System.Linq;

namespace Adidy.Services
{
    public class PaiementAdidyService(FiangonanaContext fiangonana, IMpandrayService mpandray) : IPaiementAdidyService
    {
        private readonly FiangonanaContext fiangonanaContext = fiangonana;
        private readonly IMpandrayService mpandrayService = mpandray;


        public async Task BulkInsert(IEnumerable<PaiementAdidy> liste_paiement_adidy)
        {
            HashSet<int> numeroMpandray = new(await mpandrayService.GetAllNumeroMpandray());
            IEnumerable<PaiementAdidy> liste_final = liste_paiement_adidy.Where(p => numeroMpandray.Contains((int)p.NumeroMpandray!)).ToList();
            await fiangonanaContext.BulkInsertAsync(liste_final);
            await fiangonanaContext.SaveChangesAsync();
        }
    }
}
