using Adidy.Contexts;
using Adidy.Services.Interface;
using Adidy.Utils;
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
    }
}
