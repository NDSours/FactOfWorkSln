

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FactOfWork.Models.ViewModels.Identity
{
    public class UserPageModel : PageModel
    {
        [BindProperty]
        public string Id { get; set; }

        [BindProperty]
        [Required]
        public string UserName { get; set; }

        [BindProperty]
        [Required]
        public string Surname { get; set; }

        [BindProperty]
        [Required]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        public string Patr { get; set; }

        [BindProperty]
        [Required]
        public string Position { get; set; }

        [BindProperty]
        [Required]
        public string Department { get; set; }

        [BindProperty]
        public string Password { get; set; }
    }
}
