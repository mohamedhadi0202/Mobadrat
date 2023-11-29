using Microsoft.EntityFrameworkCore;
using Mobadrat.Models;
using System.Security.Cryptography.X509Certificates;

namespace Mobadrat.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Mobadra> Mobadras { get; set; }
        public DbSet<MobadraUploadfile> MobadraUploadfiles { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
