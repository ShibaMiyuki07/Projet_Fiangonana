using Modele;

namespace Adidy.Services.Interface
{
    public interface IDroitUtilisateurService
    {
        public Task Add(DroitUtilisateur droitUtilisateur);

        public Task Delete(DroitUtilisateur droitUtilisateur);

        public Task<IEnumerable<DroitUtilisateur>> DroitUtilisateursByIdUtilisateur(string idUtilisateur);

        public Task<bool> CheckDroit(string idUtilisateur, string nomdroit);
    }
}
