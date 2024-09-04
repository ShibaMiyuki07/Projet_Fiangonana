using Adidy.Contexts;
using Adidy.Exceptions;
using Adidy.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Modele;

namespace Adidy.Services
{
    public class DroitService(FiangonanaContext context) : IDroitService
    {
        private readonly FiangonanaContext context = context;

        public async Task<IEnumerable<Droit>> GetAllDroit()
        {
            return await context.Droits.ToListAsync();
        }

        public async Task<IEnumerable<Droit>> DroitNotInUtilisateur(Utilisateur? user)
        {
            if (user == null)
                throw new UtilisateurNotExistException();

            var droitsUtilisateurIds = user.DroitUtilisateurs.Where(utilisateur => utilisateur.Isvalid != false).Select(du => du.Iddroit).ToList();

            return await context.Droits
                .Where(droit => !droitsUtilisateurIds.Contains(droit.Iddroit))
                .ToListAsync();
        }
    }
}
