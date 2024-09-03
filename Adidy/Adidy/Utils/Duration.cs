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
            duree = CalculDuration(debutmois,anneeDebut,finmois,anneeFin);
            return duree;
            
        }

        public static int CalculDuration(int moisDebut,int anneeDebut,int moisFin,int anneeFin)
        {
            int duree = 0;
            if (moisDebut == moisFin && anneeDebut == anneeFin)
                duree = 1;
            else if (anneeDebut == anneeFin && moisDebut != moisFin)
                duree = (moisFin - moisDebut) + 1;
            else
            {
                if (moisFin >= moisDebut)
                {
                    if (moisFin != 12)
                        duree = ((anneeFin - anneeDebut) * 12) + Math.Abs(moisDebut - moisFin);
                    else
                        duree = (((anneeFin - anneeDebut) + 1) * 12);
                }
                else
                    duree = ((anneeFin - anneeDebut) * 12) - Math.Abs(moisDebut - moisFin);
            }
            return duree;
        }
    }
}
