using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_WPF_MSSQL_factory_chicken
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base ("DbConnectionString")
        {
        }

        public DbSet<Chicken> Chickens { get; set; }
        public DbSet<Chicken_Factory> Chickens_Factory { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
