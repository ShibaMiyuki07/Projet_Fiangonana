using Adidy.Contexts;
using Adidy.Services.Interface;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Modele;

namespace Adidy.Services
{
    public class MpandrayService(FiangonanaContext fiangonanaContext) : IMpandrayService
    {
        private readonly FiangonanaContext fiangonanaContext = fiangonanaContext;

        public async Task<IEnumerable<Mpandray>> Search(string condition)
        {
            if(string.IsNullOrWhiteSpace(condition))
            {
                return await MpandraysPaginate(1);
                
            }
            string[] toSearch = Split_condition(condition);
            return await fiangonanaContext.Mpandrays.Where(mpandray => Condition(mpandray,toSearch)).Take(20).ToListAsync();
        }

        public async Task<IEnumerable<Mpandray>> MpandraysPaginate(int page)
        {
            int toSkip = (page-1)*20;
            return await fiangonanaContext.Mpandrays.OrderBy(mpandray => mpandray.Numero).Skip(toSkip).Take(20).ToListAsync();
        }


        public async Task<Mpandray?> GetMpandrayByNumero(int numero)
        {
            return await fiangonanaContext.Mpandrays.Where(mpandray => mpandray.Numero == numero).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<int>> GetAllNumeroMpandray()
        {
            return await fiangonanaContext.Mpandrays.Select(mpandray => mpandray.Numero).ToArrayAsync();
        }

        public async Task<Mpandray?> UpdateMpandray(Mpandray mpandray)
        {
            Mpandray? toModify = await GetMpandrayByNumero(mpandray.Numero);
            if (toModify != null)
            {
                toModify!.Numero = mpandray.Numero;
                toModify!.Anarana = mpandray.Anarana;
                toModify!.Fanampiny = mpandray.Fanampiny;
                toModify!.Fonenana = mpandray.Fonenana;
                toModify!.Fokontany = mpandray.Fokontany;
            }
            else
                throw new Exception("Mpandray non existant");

            try
            {
                await fiangonanaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException!.Message.Contains("pkey"))
                {
                    throw new Exception("Efa misy mpandray manana io numerao io");
                }
            }
            return toModify;
        }

        public async Task BulkInsert(IEnumerable<Mpandray> mpandrays)
        {
            await fiangonanaContext.BulkInsertAsync(mpandrays);
            await fiangonanaContext.SaveChangesAsync();
        }

        public async Task InsertOne(Mpandray mpandray)
        {
            try
            {
                await fiangonanaContext.AddAsync(mpandray);
                await fiangonanaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if(ex.InnerException!.Message.Contains("pkey"))
                {
                    throw new Exception("Efa misy mpandray manana io numerao io");
                }
            }
        }

        #region private function
        private static bool Condition(Mpandray mpandray,string[] toFind)
        {
            bool retour = false;
            foreach (var item in toFind)
            {
                int result = StringToInt(item);
;                if(item == mpandray.Anarana || item == mpandray.Fanampiny ||  result == mpandray.Numero)
                {
                    retour = true;
                }
            }
            return retour;
        }

        private static int StringToInt(string str)
        {
            try
            {
                return Int32.Parse(str);
            }
            catch { return 0; }
        }

        public static string[] Split_condition(string condition)
        {
            return condition.Split(' ');
        }

        #endregion
    }
}
