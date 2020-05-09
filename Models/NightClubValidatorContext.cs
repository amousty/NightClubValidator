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
            modelBuilder.Entity<IdCard>().HasKey(i => i.IdCardId);
        }

        public virtual DbSet<IdCard> IdCards { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberCard> MemberCards { get; set; }

    }
}
