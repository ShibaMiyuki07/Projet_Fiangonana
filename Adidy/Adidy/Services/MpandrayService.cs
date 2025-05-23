﻿using Adidy.Contexts;
using Adidy.Services.Interface;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Modele;

namespace Adidy.Services
{
    public class MpandrayService(FiangonanaContext fiangonanaContext) : IMpandrayService
    {
        private readonly FiangonanaContext fiangonanaContext = fiangonanaContext;

        public async Task<IEnumerable<Mpandray>> Search(int page, string condition)
        {
            if (string.IsNullOrWhiteSpace(condition))
            {
                return await MpandraysPaginate(1);

            }
            string[] toSearch = Split_condition(condition);
            return await fiangonanaContext.Mpandrays
                .Where(mpandray => toSearch.Any(ts => mpandray.Anarana!.Contains(ts)) || toSearch.Any(ts => mpandray.Fanampiny!.Contains(ts)) || toSearch.Any(ts => mpandray.Numero!.ToString().Contains(ts)))
                .Skip((page - 1) * 20)
                .Take(20)
                .Include(c => c.PaiementAdidies).Include(c => c.PaiementIsantaonas)
                .ToListAsync();
        }

        public async Task<IEnumerable<Mpandray>> MpandraysPaginate(int page)
        {
            int toSkip = (page - 1) * 20;
            return await fiangonanaContext.Mpandrays.OrderBy(mpandray => mpandray.Numero).Skip(toSkip).Take(20).Include(c => c.PaiementAdidies).Include(c => c.PaiementIsantaonas).ToListAsync();
        }


        public async Task<Mpandray?> GetMpandrayByNumero(int numero)
        {
            return await fiangonanaContext.Mpandrays.Where(mpandray => mpandray.Numero == numero).Include(c => c.PaiementAdidies).Include(c => c.PaiementIsantaonas).FirstOrDefaultAsync();
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
                if (ex.InnerException!.Message.Contains("pkey"))
                {
                    throw new Exception("Efa misy mpandray manana io numerao io");
                }
            }
        }

        #region private function


        public static string[] Split_condition(string condition)
        {
            return condition.Split(' ');
        }

        #endregion
    }
}
