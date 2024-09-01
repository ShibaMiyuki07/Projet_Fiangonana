using Modele;
using NetTopologySuite.Index.HPRtree;

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



        public IEnumerable<PaiementAdidy> CsvToPaiement(IEnumerable<CsvAdidy> listes)
        {
            List<PaiementAdidy> toReturn = [];

            foreach(var item in listes)
            {
                if (item.Adidy != decimal.Zero)
                {
                    
                    /*toAdd*/
                }
            }
            return toReturn;
        }

        public PaiementAdidy CsvToPaiementAdidy(CsvAdidy ligne)
        {
            PaiementAdidy toAdd = new()
            {
                NumeroMpandray = ligne.Numero,
                Montant = ligne.Adidy
            };

            
            return toAdd;
        }

    }



}
