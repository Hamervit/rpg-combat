namespace RPG.Combat.Dominio;

public class Personaje
{
    public string Type { get; private set; }
    public decimal Health { get; private set; }
    public decimal MaxHealth { get; private set; }
    public decimal Damage { get; private set; }
    public decimal Defense { get; private set; }
    public decimal Healing { get; private set; }
    public int Level { get; private set; } = 1;

    public Personaje(string type)
    {
        ArgumentException.ThrowIfNullOrEmpty(type);

        if (type is "InvalidType")
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

    private void ObtenerStatsBasePorTipo(string type)
    {
        switch (type)
        {
            case "Guerrero":
                MaxHealth = 1_150;
                Damage = 360;
                Defense = 165;
                Healing = 63;
                break;
            case "Tanque":
                MaxHealth = 1_300;
                Damage = 270;
                Defense = 187.5m;
                Healing = 56;
                break;
            case "Mago":
                MaxHealth = 900;
                Damage = 270;
                Defense = 187.5m;
                Healing = 56;
                break;
            case "Asesino":
                MaxHealth = 950;
                Damage = 405;
                Defense = 127.5m;
                Healing = 63;
                break;
            case "Sanador":
                MaxHealth = 900;
                Damage = 255;
                Defense = 142.5m;
                Healing = 98;
                break;
        }
    }
}