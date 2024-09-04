using System.Text.Json.Serialization;

namespace Modele;

public partial class DroitUtilisateur
{
    public string IddroitUtilisateur { get; set; } = null!;

    public int? Iddroit { get; set; }

    public string? Idutilisateur { get; set; }

    public bool? Isvalid { get; set; } = false;

    [JsonIgnore]
    public virtual Droit? IddroitNavigation { get; set; }

    [JsonIgnore]
    public virtual Utilisateur? IdutilisateurNavigation { get; set; }
}
