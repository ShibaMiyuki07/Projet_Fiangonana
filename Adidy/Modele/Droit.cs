namespace Modele;

public partial class Droit
{
    public int Iddroit { get; set; }

    public string? Typedroit { get; set; }

    public virtual ICollection<DroitUtilisateur> DroitUtilisateurs { get; set; } = [];
}
