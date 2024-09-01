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

            }
            else
            {
                 
            }
            return duree;
        }
    }
}
