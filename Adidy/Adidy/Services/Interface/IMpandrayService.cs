using Modele;

namespace Adidy.Services.Interface
{
    public interface IMpandrayService
    {
        public Task<IEnumerable<Mpandray>> Search(string condition);

        public Task<IEnumerable<Mpandray>> MpandraysPaginate(int page);

        public Task BulkInsert(IEnumerable<Mpandray> mpandrays);

        public Task<Mpandray?> GetMpandrayByNumero(int numero);

        public Task<Mpandray?> UpdateMpandray(Mpandray mpandray);

        public Task InsertOne(Mpandray mpandray);
    }
}
