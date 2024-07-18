namespace Pokedex.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public int TypeId { get; set; }
        public int MovesId { get; set; }
        public string? Description { get; set;}

        public List<PokemonMove> PokemonMoves { get; set; } = new List<PokemonMove>();
        public List<PokemonType> PokemonTypes { get; set; } = new List<PokemonType>();
    }
}
