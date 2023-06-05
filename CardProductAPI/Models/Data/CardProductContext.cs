using Microsoft.EntityFrameworkCore;

namespace CardProductAPI.Models.Data;

public class CardProductContext : DbContext
{
    public CardProductContext(DbContextOptions options) : base(options) {}
    public DbSet<Card> Cards { get; set; }
    public DbSet<Contract> Contract { get; set; }
    public DbSet<CreditLine> CreditLine { get; set; }
}