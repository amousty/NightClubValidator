using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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
            // IdCards has 2 PK, the ID and the national ID
            modelBuilder.Entity<IdCard>().HasKey(i => i.IdCardId);
        }

        // All the table we will manipulate
        public virtual DbSet<IdCard> IdCards { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberCard> MemberCards { get; set; }
    }
}
