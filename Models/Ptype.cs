using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models
{
    public class Ptype
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        
        public List<PokemonType> PokemonTypes { get; set; } = new List<PokemonType>();
    }
}
