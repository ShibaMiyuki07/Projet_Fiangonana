using Adidy.Contexts;
using Adidy.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Modele;

namespace Adidy.Services
{
    public class PaiementIsantaonaService(FiangonanaContext context, IMpandrayService mpandrayService) : IPaiementIsantaonaService
    {
        private readonly FiangonanaContext fiangonanaContext = context;
        private readonly IMpandrayService mpandrayService = mpandrayService;
        public async Task BulkInsert(IEnumerable<PaiementIsantaona> liste_paiement_adidy)
        {
            HashSet<int> numeroMpandray = new(await mpandrayService.GetAllNumeroMpandray());
            IEnumerable<PaiementIsantaona> liste_final = liste_paiement_adidy.Where(p => numeroMpandray.Contains((int)p.NumeroMpandray!)).ToList();
            await fiangonanaContext.AddRangeAsync(liste_final);
            await fiangonanaContext.SaveChangesAsync();
        }

        public async Task AddPaiement(PaiementIsantaona paiementIsantaona)
        {
            await fiangonanaContext.AddAsync(paiementIsantaona);
            await fiangonanaContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaiementIsantaona>> GetByDate(DateTime debut,DateTime fin)
        {
            return await fiangonanaContext.PaiementIsantaonas.Where(p => p.Dateheurepaiemet >= debut && p.Dateheurepaiemet <= fin).ToListAsync();
        }
    }
}
