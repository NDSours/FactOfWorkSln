using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactOfWork.Models
{
    public class LogTable
    {
        [Key]
        public long Id { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateAction { get; set; }
        public string DescriptionAction { get; set; }
        

    }
}
