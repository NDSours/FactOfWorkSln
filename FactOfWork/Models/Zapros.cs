using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactOfWork.Models
{
    public class Zapros
    {
        [Key]
        public long Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataQuery { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataResponse { get; set; }
        public string RegNumberOrganization { get; set; }
        public string NameOrganization { get; set; }
        public string AddressOrganization { get; set; }
        public string NumberProtocol { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateProtocol { get; set; }
        public string Note { get; set; }
        public string PathFileName { get; set; }
        public long PersonId { get; set; }
        public Person Person { get; set; }
        public string UserNameModify { get; set; }
    }
}
