namespace RPG.Combat.Dominio;

public class Personaje
{
    public TipoPersonaje Type { get; private set; }
    public decimal Health { get; private set; }
    public decimal MaxHealth { get; private set; }
    public decimal Damage { get; private set; }
    public decimal Defense { get; private set; }
    public decimal Healing { get; private set; }
    public int Level { get; private set; } = 1;
    public List<string> Factions { get; private set; } = [];

    public Personaje(TipoPersonaje type)
    {
        if (!Enum.IsDefined(type))
            throw new ArgumentException("Tipo de personaje no válido");

        ObtenerStatsBasePorTipo(type);

        Type = type;
        Health = MaxHealth;
    }


    public void RecibirDaño(Personaje atacante)
    {
        ArgumentNullException.ThrowIfNull(atacante);

        Health -= Math.Abs(Defense - atacante.Damage);
    }

    public void Atacar(Personaje personaje)
    {
        ArgumentNullException.ThrowIfNull(personaje);

        if (this == personaje)
            throw new InvalidOperationException("No puedes atacarte a ti mismo");

        if (ValidarEstadoVidaPersonaje() is false)
            throw new InvalidOperationException("Un personaje muerto no puede realizar daño");

        personaje.RecibirDaño(this);
    }

    private bool ValidarEstadoVidaPersonaje() => Health > 0;

    private void ObtenerStatsBasePorTipo(TipoPersonaje type)
    {
        switch (type)
        {
            case TipoPersonaje.Guerrero:
                MaxHealth = 1_150;
                Damage = 360;
                Defense = 165;
                Healing = 63;
                break;
            case TipoPersonaje.Tanque:
                MaxHealth = 1_300;
                Damage = 270;
                Defense = 187.5m;
                Healing = 56;
                break;
            case TipoPersonaje.Mago:
                MaxHealth = 900;
                Damage = 375;
                Defense = 135m;
                Healing = 77;
                break;
            case TipoPersonaje.Asesino:
                MaxHealth = 950;
                Damage = 405;
                Defense = 127.5m;
                Healing = 63;
                break;
            case TipoPersonaje.Sanador:
                MaxHealth = 900;
                Damage = 255;
                Defense = 142.5m;
                Healing = 98;
                break;
        }
    }

    public void Curar(Personaje? personaje = null)
    {
        if (ValidarEstadoVidaPersonaje() is false)
            throw new InvalidOperationException("Un personaje muerto no puede curarse");

        if (personaje != null)
        {
            var esAliado = Factions.Any(faccion => personaje.Factions.Contains(faccion));

            if (esAliado)
            {
                personaje.Health += Healing;
            }
            else
            {
                throw new InvalidOperationException("No puedes curar a un personaje que no pertenece a tu facción");
            }
        }

        var healthWithHealing = Health + Healing;

        if (healthWithHealing > MaxHealth)
            Health = MaxHealth;
        else
            Health += Healing;
    }

    public void UnirseAFaccion(string faccion)
    {
        ArgumentException.ThrowIfNullOrEmpty(faccion);

        if (Factions.Contains(faccion))
            throw new InvalidOperationException("El personaje ya pertenece a la facción");

        Factions.Add(faccion);
    }


    public void AbandonarFaccion(string faccion)
    {
        ArgumentException.ThrowIfNullOrEmpty(faccion);

        if (Factions.Contains(faccion) is false)
            throw new InvalidOperationException("El personaje no pertenece a la facción");

        Factions.Remove(faccion);
    }
}