using Adidy.Utils;
using Modele;
using NetTopologySuite.Index.HPRtree;

namespace Adidy.Models
{
    public class CsvAdidy
    {
        public string? Daty {  get; set; }

        public int Numero { get; set; }

        public string? Anarana { get; set; }

        public string Ikt {  get; set; } = string.Empty;

        public string Adidy { get; set; } = string.Empty ;

        public string MoisDebut {  get; set; } = string.Empty;

        public int AnneeDebut { get; set; }

        public string MoisFin { get; set; } = string.Empty ;

        public int AnneeFin { get; set; }



        public async Task<IEnumerable<PaiementAdidy>> CsvToPaiement(IEnumerable<CsvAdidy> listes)
        {
            List<PaiementAdidy> toReturn = [];
            await Task.Run( () =>
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

        public static PaiementAdidy CsvToPaiementAdidy(CsvAdidy ligne)
        {
            PaiementAdidy toAdd = new()
            {
                Dateheurepaiemet = DateTimeChanger.StringToDateTime(ligne.Daty!) ,
                NumeroMpandray = ligne.Numero,
                Montant = decimal.Parse(ligne.Adidy),
                AnneeDebut = ligne.AnneeDebut,
                AnneeFin = ligne.AnneeFin,
                Duree = Duration.CalculDuration(ligne.MoisDebut, ligne.AnneeDebut, ligne.MoisFin, ligne.AnneeFin),
                MoisDebut = Constante.DictionnaireMois[ligne.MoisDebut.ToLower()],
                MoisFin = Constante.DictionnaireMois[ligne.MoisFin.ToLower()],
            };
            return toAdd;
        }

    }



}
