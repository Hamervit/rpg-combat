using FluentAssertions;
using RPG.Combat.Dominio;

namespace RPG.Combat.Tests;

public class CharacterTests
{
    private readonly Personaje _guerrero = new(TipoPersonaje.Guerrero);
    private readonly Personaje _asesino = new(TipoPersonaje.Asesino);
    private readonly Personaje _sanador = new(TipoPersonaje.Sanador);

    [Fact]
    public void
        Si_UnPersonajeEsCreadoConUnTipoInválido_Debe_ArrojarUnArgumentExceptionConMensajeTipoDePersonajeNoValido()
    {
        var caller = () => new Personaje((TipoPersonaje)(99));

        caller.Should().ThrowExactly<ArgumentException>().WithMessage("Tipo de personaje no válido");
    }

    [Fact]
    public void Si_UnPersonajeDeTipoGuerreroEsCreado_Debe_TenerLasEstadisticasDeUnGuerrero()
    {
        _guerrero.Type.Should().Be(TipoPersonaje.Guerrero);
        _guerrero.MaxHealth.Should().Be(1_150);
        _guerrero.Level.Should().Be(1);
        _guerrero.Damage.Should().Be(360);
        _guerrero.Defense.Should().Be(165);
        _guerrero.Healing.Should().Be(63);
    }

    [Fact]
    public void Si_UnPersonajeDeTipoTanqueEsCreado_Debe_TenerLasEstadisticasDeUnTanque()
    {
        var tanque = new Personaje(TipoPersonaje.Tanque);

        tanque.Type.Should().Be(TipoPersonaje.Tanque);
        tanque.MaxHealth.Should().Be(1_300);
        tanque.Level.Should().Be(1);
        tanque.Damage.Should().Be(270);
        tanque.Defense.Should().Be(187.5m);
        tanque.Healing.Should().Be(56);
    }

    [Theory]
    [InlineData(TipoPersonaje.Mago, 900, 375, 135, 77)]
    [InlineData(TipoPersonaje.Asesino, 950, 405, 127.5, 63),]
    [InlineData(TipoPersonaje.Sanador, 900, 255, 142.5, 98)]
    public void Si_UnPersonajeEsCreadoConUnTipoValido_Debe_TenerLasEstadisticasDeUnPersonajeDeSuTipo(TipoPersonaje type,
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
        _guerrero.RecibirDaño(_asesino);
        _guerrero.RecibirDaño(_asesino);
        _guerrero.RecibirDaño(_asesino);
        _guerrero.RecibirDaño(_asesino);
        _guerrero.RecibirDaño(_asesino);

        var caller = () => _guerrero.Atacar(_asesino);

        caller.Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("Un personaje muerto no puede realizar daño");
    }

    [Fact]
    public void
        Si_UnPersonajeIntentaAtacarAUnPersonajeNoValido_Debe_ArrojarUnArgumentNullException()
    {
        var caller = () => _guerrero.Atacar(null!);

        caller.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void
        Si_UnPersonajeIntentaHacerseDañoASiMismo_Debe_ArrojarUnInvalidOperationExceptionConMensajeNoPuedesAtacarteATiMismo()
    {
        var caller = () => _guerrero.Atacar(_guerrero);

        caller.Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("No puedes atacarte a ti mismo");
    }

    [Fact]
    public void
        Si_UnPersonajeRealizaDañoAOtroPersonaje_Debe_ReducirLaVidaDelPersonajeAtacadoEnLaCantidadDeDañoTeniendoEnCuentaLaDefensaDelPersonajeAtacado()
    {
        _asesino.Atacar(_guerrero);

        _guerrero.Health.Should().Be(910);
    }

    [Fact]
    public void Si_UnPersonajeRecibeDañoDeUnPersonajeInvalido_Debe_ArrojarUnArgumentNullException()
    {
        var caller = () => _guerrero.RecibirDaño(null!);

        caller.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void
        Si_UnPersonajeMuertoIntentaCurarse_Debe_ArrojarUnInvalidOperationExceptionConMensajeUnPersonajeMuertoNoPuedeCurarse()
    {
        var sanador = new Personaje(TipoPersonaje.Sanador);

        sanador.RecibirDaño(_asesino);
        sanador.RecibirDaño(_asesino);
        sanador.RecibirDaño(_asesino);
        sanador.RecibirDaño(_asesino);

        var caller = () => sanador.Curar();

        caller.Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("Un personaje muerto no puede curarse");
    }

    [Fact]
    public void Si_UnPersonajeIntentaCurarseASiMismo_Debe_AumentarSuVidaEnLaCantidadDeCuracion()
    {
        var sanador = new Personaje(TipoPersonaje.Sanador);

        sanador.RecibirDaño(_asesino);

        sanador.Curar();

        sanador.Health.Should().Be(735.5m);
    }

    [Theory]
    [InlineData(TipoPersonaje.Guerrero, 1_150)]
    [InlineData(TipoPersonaje.Tanque, 1_300)]
    [InlineData(TipoPersonaje.Mago, 900)]
    [InlineData(TipoPersonaje.Asesino, 950)]
    [InlineData(TipoPersonaje.Sanador, 900)]
    public void Si_UnPersonajeRecienCreadoIntentaCurarseASiMismoSuVida_NoDebe_SerMayorASuVidaMaxima(TipoPersonaje tipo,
        decimal expectedHealth)
    {
        var personaje = new Personaje(tipo);

        personaje.Curar();

        personaje.Health.Should().Be(expectedHealth);
    }

    [Fact]
    public void Si_UnPersonajeIntentaUnirseAUnaFaccionInvalida_Debe_ArrojarUnArgumentNullException()
    {
        var caller = () => _guerrero.UnirseAFaccion(null!);

        caller.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Si_UnPersonajeSeUneAUnaFaccion_Debe_PertenecerAEsaFaccion()
    {
        _guerrero.UnirseAFaccion("Los Guerreros");

        _guerrero.Factions.Should().BeEquivalentTo(["Los Guerreros"]);
    }

    [Fact]
    public void
        Si_UnPersonajeIntentaUnirseAUnaFaccionALaQueYaPertenece_Debe_ArrojarUnInvalidOperationExceptionConMensajeElPersonajeYaPerteneceALaFaccion()
    {
        _guerrero.UnirseAFaccion("Los Guerreros");

        var caller = () => _guerrero.UnirseAFaccion("Los Guerreros");

        caller.Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("El personaje ya pertenece a la facción");
    }

    [Fact]
    public void Si_UnPersonajeIntentaAbandonarUnaFaccionInvalida_Debe_ArrojarUnArgumentNullException()
    {
        var caller = () => _guerrero.AbandonarFaccion(null!);

        caller.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void
        Si_UnPersonajeIntentaAbandonarUnaFaccionALaQueNoPertenece_Debe_ArrojarUnInvalidOperationExceptionConMensajeElPersonajeNoPerteneceALaFaccion()
    {
        var caller = () => _guerrero.AbandonarFaccion("Los Guerreros");

        caller.Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("El personaje no pertenece a la facción");
    }

    [Fact]
    public void Si_UnPersonajeAbandonaUnaFaccion_Debe_DejarDePertenecerAEsaFaccion()
    {
        _guerrero.UnirseAFaccion("Los guerreros");

        _guerrero.AbandonarFaccion("Los guerreros");

        _guerrero.Factions.Should().BeEmpty();
    }

    [Fact]
    public void
        Si_UnPersonajeIntentaCurarAOtroYPertenecenALaMismaFaccion_Debe_AumentarLaVidaDelPersonajeCuradoEnLaCantidadDeCuracion()
    {
        _sanador.UnirseAFaccion("Los Escarlatas");
        _asesino.UnirseAFaccion("Los Escarlatas");
        _asesino.RecibirDaño(_guerrero);

        _sanador.Curar(_asesino);

        _asesino.Health.Should().Be(815.5m);
    }

    [Fact]
    public void
        Si_UnPersonajeIntentaCurarAOtroYNoPertenecenALaMismaFaccion_Debe_ArrojarUnInvalidOperationExceptionConMensajeNoPuedesCurarAUnPersonajeQueNoPerteneceATuFaccion()
    {
        var caller = () => _sanador.Curar(_asesino);

        caller.Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("No puedes curar a un personaje que no pertenece a tu facción");
    }
}