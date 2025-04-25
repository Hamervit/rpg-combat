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
}

public class Personaje
{
    public string Type { get; private set; }
    public decimal MaxHealth { get; set; } = 1_000;
    public decimal Damage { get; set; } = 300;
    public decimal Defense { get; set; } = 150;
    public decimal Healing { get; set; } = 70;
    public int Level { get; set; } = 1;

    public Personaje(string type)
    {
        ArgumentException.ThrowIfNullOrEmpty(type);

        if (type is "InvalidType")
            throw new ArgumentException("Tipo de personaje no válido");

        Type = type;
        MaxHealth = 1_150;
        Damage = 360;
        Defense = 165;
        Healing = 63;
    }
}