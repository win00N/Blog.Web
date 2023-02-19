using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages
{
    public class RegisterModel : PageModel
    {
		private readonly UserManager<IdentityUser> _userManager;

		[BindProperty]
        public Register RegisterViewModel { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager)
        {
			_userManager = userManager;
		}

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = new IdentityUser()
            {
                UserName = RegisterViewModel.Login,
                Email = RegisterViewModel.Email,
            };

            var identityResult = await _userManager.CreateAsync(user, RegisterViewModel.Password);

            if(identityResult.Succeeded) 
            {
                var addRolesResult = await _userManager.AddToRoleAsync(user, "User");
                
                if (addRolesResult.Succeeded)
                {

                    ViewData["Notification"] = new Notification
                    {
                        Type = Enums.NotificationType.Success,
                        Message = "Регистрация прошла успешно"
                    };

                    return Page();
                }

            }

			ViewData["Notification"] = new Notification
			{
				Type = Enums.NotificationType.Error,
				Message = "Регистрация не удалась"
			};

            return Page();
		}
    }
}
