namespace Pokedex.Models
{
    public class PokemonMove
    {
        public int PokemonId { get; set; }
        public Pokemon Pokemon {  set; get; }

        public int MoveId { get; set; }
        public Moves Moves { get; set; }
    }
}
