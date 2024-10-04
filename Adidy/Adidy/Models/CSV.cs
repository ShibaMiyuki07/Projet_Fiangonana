using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Data
{
    internal class CSV<T>
    {
        private IEnumerable<T>? line;

        CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header!.ToLower(),
            MissingFieldFound = null,

            HeaderValidated = null,
        };

        public async Task<IEnumerable<T>> ImportFromIFormFile(IFormFile file,string delimiter)
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
                    config.Delimiter = delimiter;
                    line = await RetrieveCsv(stream);

                    if (!line.Any()) throw new Exception("La liste est vide");
                }
                catch
                {
                    throw;
                }

                return line!;
            });
        }

        private async Task<IEnumerable<T>?> RetrieveCsv(Stream stream)
        {
            return await Task.Run(() =>
            {
                using (var reader = new StreamReader(stream))
                {
                    var csv = new CsvReader(reader, config);
                    try
                    {
                        line = csv.GetRecords<T>().ToList();
                    }
                    catch (CsvHelperException)
                    {
                        throw;
                    }
                }
                return line;
            });
        }
    }
}
