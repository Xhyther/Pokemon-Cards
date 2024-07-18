namespace Pokedex.Models
{
    public class PokemonType
    {
        public int PokemonId { get; set; }
        public Pokemon Pokemon { set; get; }

        public int TypeId {  get; set; }
        public Ptype Type { get; set; }
    }
}
