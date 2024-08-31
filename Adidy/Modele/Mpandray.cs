using System;
using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace Modele;

public partial class Mpandray
{
    private string _tel = string.Empty;

    [Name("numero")]
    public int Numero { get; set; }
    [Name("anarana")]
    public string? Anarana { get; set; }
    [Name("fanampiny")]
    public string? Fanampiny { get; set; }
    [Name("fonenana")]
    public string? Fonenana { get; set; }
    [Name("fokontany")]
    public string? Fokontany { get; set; }

    public string? Tel
    {
        get
        {
            return _tel;
        }
        set
        {
            CheckNumero(value!, out _tel);
        }
    }

    private static void CheckNumero(string value, out string retour)
    {
        retour = value;
        if (!value.Split('-')[0].StartsWith('0'))
        {
            retour = '0' + value;
        }
        if (value.Split('-').Length > 1)
        {
            retour = value.Split("-")[0];
        }
    }

    public virtual ICollection<PaiementAdidy> PaiementAdidies { get; set; } = [];

    public virtual ICollection<PaiementIsantaona> PaiementIsantaonas { get; set; } = [];
}
