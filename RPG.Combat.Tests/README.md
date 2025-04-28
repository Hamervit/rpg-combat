# KATA

https://sammancoaching.org/kata_descriptions/rpg_combat.html

## Definiciones

### Daño y salud

* Todos los personajes son creados con 1000 de vida, 300 puntos de daño, 150 de defensa y 70 puntos de curación. Pueden estar vivos o muertos y deben tener un tipo.
* Los personajes pueden realizar daño a otros.
* Cuando un personaje recibe daño y su vida llega a 0, el personaje muere.
* Un personaje no debe hacerse daño a si mismo.
* Un personaje puede curarse a si mismo.
* Los personajes muertos no pueden curarse.

### Niveles

* Todos los personajes empiezan en el nivel 1.
* Para un personaje base subir de nivel aumenta sus estadísticas:
  * Vida: +10% por nivel.
  * Daño: +5% por nivel.
  * Defensa: +7% por nivel.
  * Curación: +5% por nivel.
* Si el objetivo del ataque de un personaje es otro 5 o más niveles por encima, el daño del ataque se reduce en un 50%.
* Si el objetivo del ataque de un personaje es otro personaje 5 o más niveles por debajo, el daño del ataque aumenta en
  un 50%.
* El nivel máximo de un personaje es 10.
* Los personajes de nivel 1 que sobrevivan a 1000 puntos de daño ganarán un nivel.
  * Los personajes de nivel 2 que sobrevivan a 2000 puntos de daño ganarán un nivel.
  * Los personajes de nivel 3 que sobrevivan a 3000 puntos de daño ganarán un nivel.

## Facciones

* Los personajes pueden pertenecer a una o más facciones
* Los personajes nuevos no pertenecen a ninguna facción.
* Un personaje puede unirse o abandonar una o más facciones.
* Los jugadores que pertenezcan a la misma facción son considerados aliados.
* Los aliados pueden curarse entre si.
* Los aliados no pueden hacerse daño entre si.

## Tipos de personaje

* Los personajes pueden tener un tipo, lo que determina el daño que pueden hacer, su defensa, su salud y su upgrade de stats cuando suben de nivel:
    * Guerrero:
      * Vida: +15% de la vida base.
      * Daño: +20% del daño base.
      * Defensa: +10% de la defensa base.
      * Curación: -10% de la curación base.
      * Sube de nivel:
        * Vida: +12% por nivel.
        * Daño: +7% por nivel.
        * Defensa: +5% por nivel.
    * Tanque:
      * Vida: +30% de la vida base.
      * Daño: -10% del daño base.
      * Defensa: +25% de la defensa base.
      * Curación: -20% de la curación base.
      * Sube de nivel:
        * Vida: +15% por nivel.
        * Daño: +3% por nivel.
        * Defensa: +10% por nivel.
        * Curación: +2% por nivel.
    * Mago:
      * Vida: -10% de la vida base.
      * Daño: +25% del daño base.
      * Defensa: -10% de la defensa base.
      * Curación: +10% de la curación base.
      * Sube de nivel:
        * Vida: +8% por nivel.
        * Daño: +10% por nivel.
        * Defensa: +4% por nivel.
        * Curación: +6% por nivel.
    * Asesino:
      * Vida: -5% de la vida base.
      * Daño: +35% del daño base.
      * Defensa: -15% de la defensa base.
      * Curación: -10% de la curación base.
      * Sube de nivel:
        * Vida: +9% por nivel.
        * Daño: +12% por nivel.
        * Defensa: +3% por nivel.
        * Curación: +2% por nivel.
    * Sanador:
      * Vida: -10% de la vida base.
      * Daño: -15% del daño base.
      * Defensa: -5% de la defensa base.
      * Curación: +40% de la curación base.
      * Sube de nivel:
        * Vida: +7% por nivel.
        * Daño: +2% por nivel.
        * Defensa: +4% por nivel.
        * Curación: +10% por nivel.

## Test list
- [X] Si un personaje es creado sin tipo debe arrojar un ArgumentNullException.
- [X] Si un personaje es creado con un tipo inválido debe arrojar un ArgumentException.
- [X] Si un personaje de tipo guerrero es creado debe tener las estadísticas de un guerrero.
- [X] Si un personaje de tipo tanque es creado debe tener las estadísticas de un tanque.
- [X] Si un personaje es creado con un tipo válido debe tener las estadísticas de un personaje de su tipo.
- [X] Si un personaje muerto intenta atacar debe arrojar un InvalidOperationException con mensaje Un personaje muerto no puede realizar daño.
- [X] Si un personaje intenta atacar a un personaje no válido debe arrojar un ArgumentNullException.
- [X] Si un personaje intenta hacerse daño a si mismo debe arrojar un InvalidOperationException con mensaje No puedes atacarte a ti mismo.
- [X] Si un personaje realiza daño a otro personaje debe reducir la vida del personaje atacado en la cantidad de daño teniendo en cuenta la defensa del personaje atacado.
- [X] Si un personaje recibe daño de un personaje inválido debe arrojar un ArgumentNullException.
- [X] Si un personaje muerto intenta curarse debe arrojar un InvalidOperationException con mensaje Un personaje muerto no puede curarse.
- [X] Si un personaje intenta curarse a si mismo debe aumentar su vida en la cantidad de curación.
- [X] Si un personaje recién creado intenta curarse a si mismo su vida no debe ser mayor a su vida maxima.
- [X] Si un personaje intenta unirse a una facción inválida debe arrojar un ArgumentNullException.
- [ ] Si un personaje se une a una facción debe pertenecer a esa facción.
- [ ] Si un personaje intenta unirse a una facción a la que ya pertenece debe arrojar un InvalidOperationException con mensaje El personaje ya pertenece a la facción.
- [ ] Si un personaje intenta abandonar una facción inválida debe arrojar un ArgumentNullException.
- [ ] Si un personaje intenta abandonar una facción a la que no pertenece debe arrojar un InvalidOperationException con mensaje El personaje no pertenece a la facción.
- [ ] Si un personaje abandona una facción debe dejar de pertenecer a esa facción.
- [ ] Si un personaje intenta curar a otro y pertenecen a la misma facción debe aumentar la vida del personaje curado en la cantidad de curación.
- [ ] Si un personaje intenta curar a otro y no pertenecen a la misma facción debe arrojar un InvalidOperationException con mensaje No puedes curar a un personaje que no pertenece a tu facción.
