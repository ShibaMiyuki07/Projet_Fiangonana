using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Data
{
    internal class CSV<T>
    {
        private IEnumerable<T>? line;

        private readonly CsvConfiguration config = new(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header!.ToLower(),
            MissingFieldFound = null,
            HeaderValidated = null
        };

        public async Task<IEnumerable<T>> ImportFromFileName(string filename)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    using (var fs = new StreamReader(filename))
                    {
                        line = await RetrieveCsv(fs);
                    }
                    if (!line.Any())
                    {
                        throw new Exception("La liste est vide");
                    }
                }
                catch
                {
                    throw;
                }

                return line!;
            });
        }

        private async Task<IEnumerable<T>> RetrieveCsv(StreamReader reader)
        {
            return await Task.Run(() =>
            {
                var csv = new CsvReader(reader, config);
                line = csv.GetRecords<T>().ToList();
                return line;
            });
        }
    }
}
