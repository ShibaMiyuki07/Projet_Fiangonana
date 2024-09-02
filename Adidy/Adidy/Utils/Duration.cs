using Modele;

namespace Adidy.Utils
{
    public class Duration
    {
        public static int CalculDuration(string moisDebut, int anneeDebut, string moisFin, int anneeFin)
        {
            int duree = 0;
            int debutmois = Constante.DictionnaireMois[moisDebut.ToLower()];
            int finmois = Constante.DictionnaireMois[moisFin.ToLower()];
            if (moisDebut == moisFin && anneeDebut == anneeFin)
                duree = 1;
            else if (anneeDebut == anneeFin && moisDebut != moisFin)
                duree = (finmois - debutmois) + 1;
            else
            {
                if (finmois >= debutmois)
                {
                    if (finmois != 12)
                        duree = ((anneeFin - anneeDebut) * 12) + Math.Abs(debutmois - finmois);
                    else
                        duree = (((anneeFin - anneeDebut) + 1) * 12);
                }
                else
                    duree = ((anneeFin - anneeDebut) * 12) - Math.Abs(debutmois - finmois);
            }
            return duree;
        }
    }
}
