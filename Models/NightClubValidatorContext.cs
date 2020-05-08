using Microsoft.EntityFrameworkCore;

namespace NightClubValidator.Models
{
    public class NightClubValidatorContext : DbContext
    {
        public NightClubValidatorContext(DbContextOptions<NightClubValidatorContext> options)
            : base(options)
        {
        }

        public DbSet<IdCard> IdCard { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<CardMember> CardMember { get; set; }

    }
}
