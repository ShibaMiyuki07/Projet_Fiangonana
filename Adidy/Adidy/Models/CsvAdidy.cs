using Modele;

namespace Adidy.Models
{
    public class CsvAdidy
    {
        public string? Daty {  get; set; }

        public int Numero { get; set; }

        public string? Anarana { get; set; }

        public string? Taona { get; set; }

        public decimal Ikt {  get; set; } = decimal.Zero;

        public decimal Adidy { get; set; } = decimal.Zero;



        public IEnumerable<PaiementAdidy> CsvToPaiement(IEnumerable<CsvAdidy> csvAdidy)
        {
            List<PaiementAdidy> toReturn = new List<PaiementAdidy>();
            return toReturn;
        }

    }



}
