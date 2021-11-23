using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FactOfWork.Models.ViewModels
{
    public class LoadingFilesViewModel
    {
        public SelectList PayMonth { get; set; }
        public SelectList PayYear { get; set; }
        public SelectList ReportMonth { get; set; }
        public SelectList ReportYear { get; set; }
    }
}
