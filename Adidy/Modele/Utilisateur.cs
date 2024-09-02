using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Modele;

public partial class Utilisateur 
{
    public string Idutilisateur { get; set; } = null!;

    public string? Nomutilisateur { get; set; }

    [JsonIgnore]
    public string? Motdepasse { get; set; }

    public virtual ICollection<DroitUtilisateur> DroitUtilisateurs { get; set; } =[];

}
