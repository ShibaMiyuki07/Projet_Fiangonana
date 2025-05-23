﻿namespace Modele
{
    public static class Constante
    {
        public readonly static List<Tuple<int, string>> mois =
            [new Tuple<int,string>(1, "Janvier"),
            new Tuple<int, string>(2, "Fevrier"),
            new Tuple<int, string>(3, "Mars"),
            new Tuple<int, string>(4, "Avril"),
            new Tuple<int, string>(5, "Mai"),
            new Tuple<int, string>(6, "Juin"),
            new Tuple<int, string>(7, "Juillet"),
            new Tuple<int, string>(8, "Aout"),
            new Tuple<int, string>(9, "Septembre"),
            new Tuple<int, string>(10, "Octobre"),
            new Tuple<int, string>(11, "Novembre"),
            new Tuple<int, string>(12, "Decembre")];

        public readonly static List<Tuple<int, string>> toImport = [
            new Tuple<int,string>(1,"Adidy/Ikt"),
            new Tuple<int,string>(2,"Mpandray")];

        public readonly static List<string> type_adidy = ["Adidy","IKT"];


        public readonly static Dictionary<string, int> DictionnaireMois = new()
        {
            { "janvier" , 1 },
            { "fevrier" , 2 },
            { "mars" , 3 },
            {"avril" , 4 },
            {"mai" , 5 },
            {"juin" , 6 },
            {"juillet" , 7 },
            {"aout" , 8 },
            {"septembre" , 9 },
            {"octobre" , 10 },
            {"novembre" , 11 },
            {"decembre" , 12 }
        };
    }
}
