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
    public void Si_UnPersonajeEsCreadoConUnTipoInválido_Debe_ArrojarUnArgumentException()
    {
        var caller = () => new Personaje("InvalidType");
        
        caller.Should().ThrowExactly<ArgumentException>();
    }
}

public class Personaje
{
    public Personaje(object o)
    {
        throw new ArgumentNullException();
    }
}