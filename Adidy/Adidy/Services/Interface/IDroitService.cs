using Modele;

namespace Adidy.Services.Interface
{
    public interface IDroitService
    {
        public Task<IEnumerable<Droit>> GetAllDroit();

        public Task<IEnumerable<Droit>> DroitNotInUtilisateur(Utilisateur? user);
    }
}
