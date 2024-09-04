

using System.Text.Json.Serialization;

namespace Modele;

public partial class Utilisateur
{
    public string Idutilisateur { get; set; } = null!;

    public string? Nomutilisateur { get; set; }

    [JsonIgnore]
    public string? Motdepasse { get; set; }
    [JsonIgnore]

    public virtual ICollection<DroitUtilisateur> DroitUtilisateurs { get; set; } = [];

}
