using Microsoft.EntityFrameworkCore;
using Pokedex.Models;

namespace Pokedex.Data
{
    public class PokedexDbContext : DbContext
    {
        public PokedexDbContext(DbContextOptions<PokedexDbContext> options) : 
            base(options) 
        { 
        }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Moves> Moves { get; set; }
        public DbSet<Ptype> Types { get; set; }
        public DbSet<PokemonMove> PokemonMoves { get; set; }
        public DbSet<PokemonType> PokemonTypes {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PokemonType>()
                .HasKey(pt => new {pt.PokemonId, pt.TypeId});

            modelBuilder.Entity<PokemonType>()
                .HasOne(pt => pt.Pokemon)
                .WithMany(p => p.PokemonTypes)
                .HasForeignKey(pt => pt.PokemonId);

            modelBuilder.Entity<PokemonType>()
               .HasOne(pt => pt.Type)
               .WithMany(t => t.PokemonTypes)
               .HasForeignKey(pt => pt.TypeId);

            modelBuilder.Entity<PokemonMove>()
                .HasKey(pm => new { pm.PokemonId, pm.MoveId });

            modelBuilder.Entity<PokemonMove>()
                .HasOne(pm => pm.Pokemon)
                .WithMany(p => p.PokemonMoves)
                .HasForeignKey(pm => pm.PokemonId);

            modelBuilder.Entity<PokemonMove>()
                .HasOne(pm => pm.Moves)
                .WithMany(m => m.PokemonMoves)
                .HasForeignKey(pm => pm.MoveId);
        }
    }
}
