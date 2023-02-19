using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Blog.Web.Pages.Admin.Blogs
{
	public class EditModel : PageModel
	{
		private readonly IBlogPostRepository _blogPostRepository;

		[BindProperty]
		public BlogPost BlogPost { get; set; }

		[BindProperty]
		public IFormFile FeaturedImage { get; set; }


		public EditModel(IBlogPostRepository blogPostRepository)
		{
			_blogPostRepository = blogPostRepository;
		}

		public async Task OnGet(Guid id)
		{
			BlogPost = await _blogPostRepository.GetAsync(id);
		}

		public async Task<IActionResult> OnPostEdit()
		{
			try
			{
				await _blogPostRepository.UpdateAsync(BlogPost);

				ViewData["Notification"] = new Notification
				{
					Message = "Изменения успешно сохранены",
					Type = Enums.NotificationType.Success
				};

			}
			catch (Exception ex)
			{
				ViewData["Notification"] = new Notification
				{
					Message = $"Произошла ошибка {ex.Message}",
					Type = Enums.NotificationType.Error
				};
			}

			return Page();
		}

		public async Task<IActionResult> OnPostDelete()
		{

			var deleted = await _blogPostRepository.DeleteAsync(BlogPost.BlogPostId);
				
			if (deleted)
			{
				var notification = new Notification
				{
					Message = "Пост успешно удален",
					Type = Enums.NotificationType.Success
				};
				TempData["Notification"] = JsonSerializer.Serialize(notification);
				return RedirectToPage("/Admin/Blogs/List");
			}

			return Page();
		}
	}
}