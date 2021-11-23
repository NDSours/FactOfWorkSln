
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FactOfWork.Models.ViewModels.Identity
{
    public class RolePageModel : PageModel
    {
        public string Id { get; set; }

        [BindProperty][Required]
        public string Name { get; set; }
    }
}
