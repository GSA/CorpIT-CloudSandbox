using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class AccessRequestContext : DbContext
    {
        public AccessRequestContext(DbContextOptions<AccessRequestContext> options)
            : base(options)
        {
        }

        public DbSet<AccessRequest> AccessRequests { get; set; }

    }
}
