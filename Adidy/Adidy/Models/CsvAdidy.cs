using Adidy.Utils;
using Modele;

namespace Adidy.Models
{
    public class CsvAdidy
    {
        public string? Daty { get; set; }

        public string Numero { get; set; } = string.Empty;

        public string? Anarana { get; set; }

        public string Ikt { get; set; } = string.Empty;

        public string Adidy { get; set; } = string.Empty;

        public string MoisDebut { get; set; } = string.Empty;

        public int AnneeDebut { get; set; }

        public string MoisFin { get; set; } = string.Empty;

        public int AnneeFin { get; set; }



        public static async Task<IEnumerable<PaiementAdidy>> CsvToPaiement(IEnumerable<CsvAdidy> listes)
        {
            List<PaiementAdidy> toReturn = [];
            await Task.Run(() =>
            {
                foreach (var item in listes)
                {
                    if (item.Adidy != string.Empty)
                    {
                        toReturn.Add(CsvToPaiementAdidy(item));
                    }
                }
            });
            return toReturn;
        }

        public static async Task<IEnumerable<PaiementIsantaona>> CsvToPaiementIsantaona(IEnumerable<CsvAdidy> listes)
        {
            List<PaiementIsantaona> toReturn = [];
            await Task.Run(() =>
            {
                foreach (var item in listes)
                {
                    if (item.Ikt != string.Empty)
                    {
                        toReturn.Add(CsvToIsantaona(item));
                    }
                }
            });
            return toReturn;
        }

        private static PaiementAdidy CsvToPaiementAdidy(CsvAdidy ligne)
        {
            PaiementAdidy toAdd = new()
            {
                Dateheurepaiemet = DateTimeChanger.StringToDateTime(ligne.Daty!),
                NumeroMpandray = ligne.Numero.Contains("RP") ? 9999 : Int32.Parse(ligne.Numero),
                Montant = decimal.Parse(ligne.Adidy),
                AnneeDebut = ligne.AnneeDebut,
                AnneeFin = ligne.AnneeFin,
                Duree = Duration.CalculDuration(ligne.MoisDebut, ligne.AnneeDebut, ligne.MoisFin, ligne.AnneeFin),
                MoisDebut = Constante.DictionnaireMois[ligne.MoisDebut.ToLower()],
                MoisFin = Constante.DictionnaireMois[ligne.MoisFin.ToLower()],
            };
            return toAdd;
        }

        private static PaiementIsantaona CsvToIsantaona(CsvAdidy ligne)
        {
            PaiementIsantaona toAdd = new()
            {
                Dateheurepaiemet = DateTimeChanger.StringToDateTime(ligne.Daty!),
                NumeroMpandray = ligne.Numero.Contains("RP") ? 9999 : Int32.Parse(ligne.Numero),
                Montant = decimal.Parse(ligne.Ikt),
                AnneeDebut = ligne.AnneeDebut,
                AnneeFin = ligne.AnneeFin
            };
            return toAdd;
        }

    }



}
