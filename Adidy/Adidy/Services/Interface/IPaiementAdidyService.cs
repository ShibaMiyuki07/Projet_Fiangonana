using Modele;

namespace Adidy.Services.Interface
{
    public interface IPaiementAdidyService
    {
        public Task BulkInsert(IEnumerable<PaiementAdidy> liste_paiement_adidy);

        public Task<IEnumerable<PaiementAdidy>> GetPaiementByNumeroMpandray(int numero);

        public Task AddPaiement(PaiementAdidy paiementAdidy);
    }
}
