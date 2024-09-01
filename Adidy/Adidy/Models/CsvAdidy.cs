using Modele;
using NetTopologySuite.Index.HPRtree;

namespace Adidy.Models
{
    public class CsvAdidy
    {
        public string? Daty {  get; set; }

        public int Numero { get; set; }

        public string? Anarana { get; set; }

        public decimal Ikt {  get; set; } = decimal.Zero;

        public decimal Adidy { get; set; } = decimal.Zero;

        public string MoisDebut {  get; set; } = string.Empty;

        public int AnneeDebut { get; set; }

        public string MoisFin { get; set; } = string.Empty ;

        public int AnneeFin { get; set; }



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
                Montant = ligne.Adidy,
                AnneeDebut = ligne.AnneeDebut,
                AnneeFin = ligne.AnneeFin,
            };
            return toAdd;
        }

    }



}
