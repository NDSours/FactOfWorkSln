using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FactOfWork.Pages.Account
{
    [Authorize(Roles="Администратор")]
    public class AccountRights : PageModel
    {
        
    }
}
