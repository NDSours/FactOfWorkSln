using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactOfWork.Models
{
    public interface IRepositoryFile
    {
        public IEnumerable<File> Files { get; }

        public void Add(Models.File file);
    }
}
