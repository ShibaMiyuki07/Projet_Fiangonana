using Adidy.Contexts;
using Microsoft.EntityFrameworkCore;
using Modele;

namespace Adidy.Services
{
    public class DroitService(FiangonanaContext context)
    {
        private readonly FiangonanaContext context = context;

        public async Task<IEnumerable<Droit>> GetAllDroit()
        {
            return await context.Droits.ToListAsync();
        }
    }
}
