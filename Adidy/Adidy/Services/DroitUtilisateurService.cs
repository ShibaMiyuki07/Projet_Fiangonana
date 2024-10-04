using Adidy.Contexts;
using Adidy.Services.Interface;
using Microsoft.EntityFrameworkCore;
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

        public async Task Delete(DroitUtilisateur droitUtilisateur)
        {
            droitUtilisateur = context.DroitUtilisateurs.Where(d => d.Idutilisateur == droitUtilisateur.Idutilisateur && d.Iddroit == droitUtilisateur.Iddroit && d.Isvalid == true).FirstOrDefault()!;
			context.Remove(droitUtilisateur);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DroitUtilisateur>> DroitUtilisateursByIdUtilisateur(string idUtilisateur)
        {
            return await context.DroitUtilisateurs.Where(du => du.Idutilisateur == idUtilisateur).Include(c => c.IddroitNavigation).ToListAsync();
        }

        public async Task<bool> CheckDroit(string idUtilisateur, string nomdroit)
        {
            bool retour = false;
            IEnumerable<DroitUtilisateur> listedroit = await DroitUtilisateursByIdUtilisateur(idUtilisateur);
            DroitUtilisateur? droitUtilisateur = listedroit.Where(c => c.IddroitNavigation!.Typedroit == nomdroit).FirstOrDefault();
            if(droitUtilisateur != null)
            {
                retour = true;
            }
            return retour;
        }
    }
}
