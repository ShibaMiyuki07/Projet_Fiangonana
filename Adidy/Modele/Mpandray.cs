using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Modele;

public partial class Mpandray
{
    private string _tel = string.Empty;

    [Required(ErrorMessage = "Ampidiro ilay numerao mpandray")]
    [Name("numero")]
    public int Numero { get; set; }


    [Required(ErrorMessage = "Ampidiro ny anaran'ilay mpandray")]
    [Name("anarana")]
    public string? Anarana { get; set; }


    [Required(ErrorMessage = "Ampidiro ilay fanampin'anarana")]
    [Name("fanampiny")]
    public string? Fanampiny { get; set; }


    [Required(ErrorMessage = "Ampidiro ny adiresy")]
    [Name("fonenana")]
    public string? Fonenana { get; set; }


    [Required(ErrorMessage = "Ampidiro ny anarana fokotany")]
    [Name("fokontany")]
    public string? Fokontany { get; set; }

    [Required(ErrorMessage = "Ampidiro ny numerao finday raha tsy misy dia atao 0")]
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
        if (string.IsNullOrEmpty(value))
        {
            value = "0";
        }
        retour = value;
    }

    public virtual ICollection<PaiementAdidy> PaiementAdidies { get; set; } = [];

    public virtual ICollection<PaiementIsantaona> PaiementIsantaonas { get; set; } = [];
}
