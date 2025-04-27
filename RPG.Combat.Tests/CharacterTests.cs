using FluentAssertions;
using RPG.Combat.Dominio;

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

    [Fact]
    public void
        Si_UnPersonajeMuertoIntentaAtacar_Debe_ArrojarUnInvalidOperationExceptionConMensajeUnPersonajeMuertoNoPuedeRealizarDaño()
    {
        var guerrero = new Personaje("Guerrero");
        var asesino = new Personaje("Asesino");
        guerrero.RecibirDaño(asesino);
        guerrero.RecibirDaño(asesino);
        guerrero.RecibirDaño(asesino);
        guerrero.RecibirDaño(asesino);
        guerrero.RecibirDaño(asesino);

        var caller = () => guerrero.Atacar(asesino);

        caller.Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("Un personaje muerto no puede realizar daño");
    }

    [Fact]
    public void
        Si_UnPersonajeIntentaAtacarAUnPersonajeNoValido_Debe_ArrojarUnArgumentNullException()
    {
        var guerrero = new Personaje("Guerrero");

        var caller = () => guerrero.Atacar(null!);

        caller.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void
        Si_UnPersonajeIntentaHacerseDañoASiMismo_Debe_ArrojarUnInvalidOperationExceptionConMensajeNoPuedesAtacarteATiMismo()
    {
        var guerrero = new Personaje("Guerrero");

        var caller = () => guerrero.Atacar(guerrero);

        caller.Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("No puedes atacarte a ti mismo");
    }

    [Fact]
    public void
        Si_UnPersonajeRealizaDañoAOtroPersonaje_Debe_ReducirLaVidaDelPersonajeAtacadoEnLaCantidadDeDañoTeniendoEnCuentaLaDefensaDelPersonajeAtacado()
    {
        var asesino = new Personaje("Asesino");
        var guerrero = new Personaje("Guerrero");

        asesino.Atacar(guerrero);

        guerrero.Health.Should().Be(910);
    }

    [Fact]
    public void Si_UnPersonajeRecibeDañoDeUnPersonajeInvalido_Debe_ArrojarUnArgumentNullException()
    {
        var guerrero = new Personaje("Guerrero");

        var caller = () => guerrero.RecibirDaño(null!);

        caller.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void
        Si_UnPersonajeMuertoIntentaCurarse_Debe_ArrojarUnInvalidOperationExceptionConMensajeUnPersonajeMuertoNoPuedeCurarse()
    {
        var sanador = new Personaje("Sanador");
        var asesino = new Personaje("Asesino");
        sanador.RecibirDaño(asesino);
        sanador.RecibirDaño(asesino);
        sanador.RecibirDaño(asesino);
        sanador.RecibirDaño(asesino);

        var caller = () => sanador.Curar();

        caller.Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("Un personaje muerto no puede curarse");
    }
}