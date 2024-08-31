using System;
using System.Collections.Generic;

namespace Modele;

public partial class PaiementIsantaona
{
    public string IdpaiementIsantaona { get; set; } = null!;

    public int? NumeroMpandray { get; set; }

    public int? AnneeDebut { get; set; }

    public int? AnneeFin { get; set; }

    public decimal? Montant { get; set; }

    public DateTime? Dateheurepaiemet { get; set; }

    public virtual Mpandray? NumeroMpandrayNavigation { get; set; }
}
