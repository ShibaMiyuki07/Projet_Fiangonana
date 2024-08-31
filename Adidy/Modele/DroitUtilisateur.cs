using System;
using System.Collections.Generic;

namespace  Modele;

public partial class DroitUtilisateur
{
    public string IddroitUtilisateur { get; set; } = null!;

    public int? Iddroit { get; set; }

    public string? Idutilisateur { get; set; }

    public bool? Isvalid { get; set; }

    public virtual Droit? IddroitNavigation { get; set; }

    public virtual Utilisateur? IdutilisateurNavigation { get; set; }
}
