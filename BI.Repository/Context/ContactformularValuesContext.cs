using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BI.Repository.Entities;

namespace BI.Repository.Context
{
    public class ContactformularValuesContext : DbContext
    {
        public DbSet<ContactformularValues> ContactformularValues { get; set; }
        public ContactformularValuesContext()
            : base("name=BIContext")
        {
            Database.SetInitializer<ContactformularValuesContext>(null);
        }

    }
}
