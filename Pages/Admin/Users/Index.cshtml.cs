using Blog.Web.Models.ViewModels;
using Blog.Web.Pages.Admin.Blogs;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages.Admin.Users
{
	[Authorize(Roles = "Admin")]
	public class IndexModel : PageModel
	{
		private readonly IUserRepository _userRepository;

		public List<User> Users { get; set; }

		[BindProperty]
		public AddUser AddUserRequest { get; set; }

		[BindProperty]
		public Guid SelectedUserId { get; set; }

		public IndexModel(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<IActionResult> OnGet()
		{
			await GetUsers();
			return Page();
		}

		public async Task<IActionResult> OnPost()
		{

			if (ModelState.IsValid)
			{
				var identityUser = new IdentityUser
				{
					UserName = AddUserRequest.UserName,
					Email = AddUserRequest.Email
				};

				var roles = new List<string>
			{
				"User",
			};

				if (AddUserRequest.AdminCheckbox)
				{
					roles.Add("Admin");
				}

				var result = await _userRepository
					.Add(identityUser, AddUserRequest.Password, roles);

				if (result)
				{
					return RedirectToPage("/Admin/Users/Index");
				}

				return Page();
			}

			await GetUsers();
			return Page();
		}

		public async Task<IActionResult> OnPostDelete()
		{
			await _userRepository.Delete(SelectedUserId);
			return RedirectToPage("/Admin/Users/Index");
		}

		private async Task GetUsers()
		{
			var users = await _userRepository.GetAll();

			Users = new List<User>();

			foreach (var user in users)
			{
				Users.Add(new User
				{
					UserId = Guid.Parse(user.Id),
					UserName = user.UserName,
					Email = user.Email,
				});
			}

		}
	}
}
