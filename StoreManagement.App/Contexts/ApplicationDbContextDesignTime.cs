using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.App.Contexts
{
    public class ApplicationDbContextDesignTime : IDesignTimeDbContextFactory<ApplicationDbContext>
    {

#if DEBUG
        private static string ConnectionString = "Server=.\\SQLEXPRESS;Database=SmartStore;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
#elif RELEASE
private static string ConnectionString = "PRODUCTION-CONNECTION-STRING";
#endif

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

            return new(optionsBuilder.Options);
        }
    }
}
