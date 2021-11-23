using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactOfWork.Models
{
    public class File
    {
        [Key]
        public long Id { get; set; }
        public string FileName { get; set; }
        public string PathLoad { get; set; }
        public string PathArhive { get; set; }

    }
}
