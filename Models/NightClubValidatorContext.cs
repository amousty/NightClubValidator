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
            // Foreign key with id card
            //modelBuilder.Entity<Member>()
            //    .HasOne(m => m.IdCard)
            //    .WithOne(i => i.Member)
            //    .HasForeignKey(m => m.idcar)
            //.OnDelete(DeleteBehavior.Cascade);

            //Foreign key with card members
            //modelBuilder.Entity<Member>()
            //    .HasOne(m => m.CardMembers)
            //    .WithMany(c => c.id)
            //    .HasForeignKey(m => m.IdCardMemberId);
            //    .OnDelete(DeleteBehavior.Cascade);
        }

        public virtual DbSet<IdCard> IdCards { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<CardMember> CardMembers { get; set; }

    }
}
