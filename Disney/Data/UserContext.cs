using Disney.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Disney.Data
{
    public class UserContext : IdentityDbContext<User>
    {
        private const string Schema = "users";

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(Schema);
        }
    }
}
