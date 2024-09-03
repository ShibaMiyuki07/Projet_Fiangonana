namespace Modele;

public partial class PaiementAdidy
{
    public string IdpaiementAdidy { get; set; } = null!;

    public int? NumeroMpandray { get; set; }

    public int? MoisDebut { get; set; }

    public int? AnneeDebut { get; set; }

    public int? MoisFin { get; set; }

    public int? AnneeFin { get; set; }

    public decimal? Montant { get; set; }

    public int? Duree { get; set; }

    public DateTime? Dateheurepaiemet { get; set; } = DateTime.Now;

    public virtual Mpandray? NumeroMpandrayNavigation { get; set; }
}
