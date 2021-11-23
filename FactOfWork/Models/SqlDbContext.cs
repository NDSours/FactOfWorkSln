using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FactOfWork.Models
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> opts) : base(opts)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Zapros> Zaproses { get; set; }
        public DbSet<LogTable> LogTables { get; set; }
    }
}
