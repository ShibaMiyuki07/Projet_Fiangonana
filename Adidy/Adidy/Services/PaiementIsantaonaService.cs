using Adidy.Contexts;
using Adidy.Services.Interface;
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
	}
}
