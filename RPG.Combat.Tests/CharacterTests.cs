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
}

public abstract class PersonajeBase
{
    protected decimal MaxHealth { get; set; } = 1_000;
    protected decimal Damage { get; set; } = 300;
    protected decimal Defense { get; set; } = 150;
    protected decimal Healing { get; set; } = 70;
    protected int Level { get; set; } = 1;
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
    }
}