using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models
{
    public class Moves
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public List<PokemonMove> PokemonMoves { get; set; } = new List<PokemonMove>();

    }
}
