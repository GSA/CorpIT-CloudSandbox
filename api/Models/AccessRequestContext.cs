using System;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class AccessRequestContext : DbContext
    {
        public AccessRequestContext(DbContextOptions<AccessRequestContext> options)
            : base(options)
        {
            //Console.WriteLine("AccessRequestsController: point 20");
        }

        public DbSet<AccessRequest> AccessRequests { get; set; }

    }
}
