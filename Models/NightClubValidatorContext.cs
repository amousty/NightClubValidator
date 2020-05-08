using Microsoft.EntityFrameworkCore;

namespace NightClubValidator.Models
{
    public class NightClubValidatorContext : DbContext
    {
        public NightClubValidatorContext(DbContextOptions<NightClubValidatorContext> options)
            : base(options)
        {
        }

        public DbSet<IdCard> IdCards { get; set; }
        public DbSet<Member> Members { get; set; }

    }
}
