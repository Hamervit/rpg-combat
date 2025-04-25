using FluentAssertions;

namespace RPG.Combat.Tests;

public class CharacterTests
{
    [Fact]
    public void Si_UnPersonajeEsCreadoSinTipo_Debe_ArrojarUnArgumentNullException()
    {
        var caller = () => new Personaje(null!);

        caller.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void
        Si_UnPersonajeEsCreadoConUnTipoInválido_Debe_ArrojarUnArgumentExceptionConMensajeTipoDePersonajeNoValido()
    {
        var caller = () => new Personaje("InvalidType");

        caller.Should().ThrowExactly<ArgumentException>().WithMessage("Tipo de personaje no válido");
    }

    [Fact]
    public void Si_UnPersonajeDeTipoGuerreroEsCreado_Debe_TenerLasEstadisticasDeUnGuerrero()
    {
        var guerrero = new Personaje("Guerrero");

        guerrero.Type.Should().Be("Guerrero");
        guerrero.MaxHealth.Should().Be(1_150);
        guerrero.Level.Should().Be(1);
        guerrero.Damage.Should().Be(360);
        guerrero.Defense.Should().Be(165);
        guerrero.Healing.Should().Be(63);
    }

    [Fact]
    public void Si_UnPersonajeDeTipoTanqueEsCreado_Debe_TenerLasEstadisticasDeUnTanque()
    {
        var tanque = new Personaje("Tanque");

        tanque.Type.Should().Be("Tanque");
        tanque.MaxHealth.Should().Be(1_300);
        tanque.Level.Should().Be(1);
        tanque.Damage.Should().Be(270);
        tanque.Defense.Should().Be(187.5m);
        tanque.Healing.Should().Be(56);
    }

    [Theory]
    [InlineData("Mago", 900, 270, 187.5, 56)]
    [InlineData("Asesino", 950, 405, 127.5, 63),]
    [InlineData("Sanador", 900, 255, 142.5, 98)]
    public void Si_UnPersonajeEsCreadoConUnTipoValido_Debe_TenerLasEstadisticasDeUnPersonajeDeSuTipo(string type,
        decimal expectedMaxHealth, decimal expectedDamage, decimal expectedDefense, decimal expectedHealing)
    {
        var personaje = new Personaje(type);

        personaje.Type.Should().Be(type);
        personaje.MaxHealth.Should().Be(expectedMaxHealth);
        personaje.Damage.Should().Be(expectedDamage);
        personaje.Defense.Should().Be(expectedDefense);
        personaje.Healing.Should().Be(expectedHealing);
        personaje.Level.Should().Be(1);
    }
}

public class Personaje
{
    public string Type { get; private set; }
    public decimal MaxHealth { get; set; }
    public decimal Damage { get; set; }
    public decimal Defense { get; set; }
    public decimal Healing { get; set; }
    public int Level { get; set; } = 1;

    public Personaje(string type)
    {
        ArgumentException.ThrowIfNullOrEmpty(type);

        if (type is "InvalidType")
            throw new ArgumentException("Tipo de personaje no válido");

        Type = type;

        ObtenerStatsBasePorTipo(type);
    }

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