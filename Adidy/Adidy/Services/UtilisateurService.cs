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
            Utilisateur? resultat = await context.Utilisateurs.Where(utilisateur => utilisateur.Nomutilisateur == user!.Nomutilisateur && utilisateur.Motdepasse == user.Motdepasse).Include(c => c.DroitUtilisateurs).FirstOrDefaultAsync();
            if (resultat == null)
                throw new UtilisateurNotExistException("Nom d'utilisateur/Mot de passe inexistant");
            return resultat!;
        }

        public async Task<IEnumerable<Utilisateur>> GetAllUtilisateur()
        {
            return await context.Utilisateurs.ToListAsync();
        }
    }
}
