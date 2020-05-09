using Microsoft.EntityFrameworkCore;

namespace NightClubValidator.Models
{
    public class NightClubValidatorContext : DbContext
    {
        public NightClubValidatorContext(DbContextOptions<NightClubValidatorContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public virtual DbSet<IdCard> IdCards { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<CardMember> CardMembers { get; set; }

    }
}
