using System;
using System.Collections.Generic;

namespace Modele;

public partial class Utilisateur
{
    public string Idutilisateur { get; set; } = null!;

    public string? Nomutilisateur { get; set; }

    public string? Motdepasse { get; set; }

    public virtual ICollection<DroitUtilisateur> DroitUtilisateurs { get; set; } =[];
}
