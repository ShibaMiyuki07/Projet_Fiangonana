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

        
        public async Task<IEnumerable<T>> ImportFromIFormFile(IFormFile file)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    if (file == null || file.Length == 0)
                    {
                        throw new ArgumentException("No file uploaded or file is empty");
                    }
                    var stream = file.OpenReadStream();
                    using (var reader = new StreamReader(stream))
                    {
                        line = await RetrieveCsv(reader);
                    }

                    if (!line.Any()) throw new Exception("La liste est vide");
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
