using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages
{
    public class LogoutModel : PageModel
    {
		private readonly SignInManager<IdentityUser> _signInManager;

		public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
			_signInManager = signInManager;
		}

        public async Task<IActionResult> OnGet()
        {
			await _signInManager.SignOutAsync();
			return RedirectToPage("Index");
		}
    }
}
