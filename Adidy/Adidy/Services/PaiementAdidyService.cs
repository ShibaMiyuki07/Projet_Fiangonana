using Adidy.Contexts;
using Adidy.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Modele;

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
            await fiangonanaContext.AddRangeAsync(liste_final);
            await fiangonanaContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaiementAdidy>> GetPaiementByNumeroMpandray(int numero)
        {
            return await fiangonanaContext.PaiementAdidies.Where(paiement => paiement.NumeroMpandray == numero).ToListAsync();
        }

        public async Task AddPaiement(PaiementAdidy paiementAdidy)
        {
            await fiangonanaContext.AddAsync(paiementAdidy);
            await fiangonanaContext.SaveChangesAsync();
        }
    }
}
