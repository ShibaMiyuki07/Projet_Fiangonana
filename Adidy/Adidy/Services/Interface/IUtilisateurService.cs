using Modele;

namespace Adidy.Services.Interface
{
    public interface IUtilisateurService
    {
        public Task CreateUtilisateur(Utilisateur user);

        public Task<Utilisateur> Login(Utilisateur user);

        public Task<IEnumerable<Utilisateur>> GetAllUtilisateur();

        public Task<Utilisateur?> GetUtilisateurById(string id);
    }
}
