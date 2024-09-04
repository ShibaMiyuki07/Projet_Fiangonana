using Adidy.Contexts;
using Adidy.Services.Interface;
using Modele;

namespace Adidy.Services
{
    public class DroitUtilisateurService(FiangonanaContext context) : IDroitUtilisateurService
    {
        private readonly FiangonanaContext context = context;

        public async Task Add(DroitUtilisateur droitUtilisateur)
        {
            await context.AddAsync(droitUtilisateur);
            await context.SaveChangesAsync();
        }
    }
}
