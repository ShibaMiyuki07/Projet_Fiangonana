using Adidy.Contexts;
using Adidy.Exceptions;
using Adidy.Services.Interface;
using Adidy.Utils;
using Microsoft.EntityFrameworkCore;
using Modele;

namespace Adidy.Services
{
    public class UtilisateurService(FiangonanaContext context) : IUtilisateurService
    {
        private readonly FiangonanaContext context = context;

        public async Task CreateUtilisateur(Utilisateur user)
        {
            try
            {
                user.Motdepasse = PasswordEncryption.StrToShA1(user.Motdepasse!);
                await context.AddAsync(user);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Utilisateur> Login(Utilisateur user)
        {
            user.Motdepasse = PasswordEncryption.StrToShA1(user?.Motdepasse!);
            Utilisateur? resultat = await context.Utilisateurs.Where(utilisateur => utilisateur.Nomutilisateur == user!.Nomutilisateur && utilisateur.Motdepasse == user.Motdepasse).FirstOrDefaultAsync();
            return resultat ?? throw new UtilisateurNotExistException("Nom d'utilisateur/Mot de passe inexistant");
        }

        public async Task<IEnumerable<Utilisateur>> GetAllUtilisateur()
        {
            return await context.Utilisateurs.ToListAsync();
        }

        public async Task<Utilisateur?> GetUtilisateurById(string id)
        {
            return await context.Utilisateurs.Where(utilisateur => utilisateur.Idutilisateur == id).Include(c => c.DroitUtilisateurs).ThenInclude(c => c.IddroitNavigation).FirstOrDefaultAsync();
        }
    }
}
