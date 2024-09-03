using Modele;

namespace Adidy.Services.Interface
{
    public interface IPaiementIsantaonaService
    {
        public Task BulkInsert(IEnumerable<PaiementIsantaona> liste_paiement_adidy);

        public Task AddPaiement(PaiementIsantaona paiementIsantaona);
    }
}
