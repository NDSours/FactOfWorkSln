using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactOfWork.Models
{
    public class RepositoryFile : IRepositoryFile
    {
        private SqlDbContext _context;
        public RepositoryFile(SqlDbContext cxt) => _context = cxt;
        public IEnumerable<File> Files => _context.Files.ToArray();

        public void Add(File file)
        {
            _context.Files.Add(file);
            _context.SaveChanges();
        }
    }
}
