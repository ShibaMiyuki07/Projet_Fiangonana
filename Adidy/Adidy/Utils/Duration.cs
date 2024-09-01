using Modele;

namespace Adidy.Utils
{
    public class Duration
    {
        public static int CalculDuration(string moisDebut,int anneeDebut,string moisFin,int anneeFin)
        {
            int duree = 0;
            if (moisDebut == moisFin && anneeDebut == anneeFin)
            {
                duree = 1;
            }
            else if(anneeDebut == anneeFin  && moisDebut != moisFin)
            {
                int debutmois = Constante.DictionnaireMois[moisDebut.ToLower()];
                int finmois = Constante.DictionnaireMois[moisFin.ToLower()];

                duree = finmois - debutmois;
            }
            else
            {
                 
            }
            return duree;
        }
    }
}
