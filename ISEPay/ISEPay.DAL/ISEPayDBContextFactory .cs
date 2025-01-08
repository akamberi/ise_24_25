/*using ISEPay.DAL.Persistence;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEPay.DAL
{
    public class ISEPayDBContextFactory : IDesignTimeDbContextFactory<ISEPayDBContext>
    {
        public ISEPayDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ISEPayDBContext>();

            // Corrected hardcoded connection string
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ISEPay;Integrated Security=True;TrustServerCertificate=True");

            return new ISEPayDBContext(optionsBuilder.Options);
        }
    }
}
*/