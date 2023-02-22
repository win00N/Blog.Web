using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Blog.Web.Pages.Admin.Blogs
{
	[Authorize(Roles = "Admin")]
	public class EditModel : PageModel
	{
		private readonly IBlogPostRepository _blogPostRepository;

		[BindProperty]
		public EditBlogPostRequest BlogPost { get; set; }

		[BindProperty]
		public IFormFile FeaturedImage { get; set; }

		[BindProperty]
		[Required]
		public string Tags { get; set; }


		public EditModel(IBlogPostRepository blogPostRepository)
		{
			_blogPostRepository = blogPostRepository;
		}

		public async Task OnGet(Guid id)
		{
			var blogPostDomainModel = await _blogPostRepository.GetAsync(id);

			if (blogPostDomainModel != null && blogPostDomainModel.Tags != null)
			{
				BlogPost = new EditBlogPostRequest
				{
					BlogPostId = blogPostDomainModel.BlogPostId,
					Heading = blogPostDomainModel.Heading,
					PageTitle = blogPostDomainModel.PageTitle,
					Content = blogPostDomainModel.Content,
					ShortDescription = blogPostDomainModel.ShortDescription,
					FeaturedImageUrl = blogPostDomainModel.FeaturedImageUrl,
					UrlHandle = blogPostDomainModel.UrlHandle,
					PublishDate = blogPostDomainModel.PublishDate,
					Author = blogPostDomainModel.Author,
					Visible = blogPostDomainModel.Visible
				};

				Tags = string.Join(',', blogPostDomainModel.Tags.Select(x => x.Name));
			}
		}

		public async Task<IActionResult> OnPostEdit()
		{
			ValidateEditBlogPost();

			if (ModelState.IsValid)
			{
				try
				{
					var blogPostDomainModel = new BlogPost
					{
						BlogPostId = BlogPost.BlogPostId,
						Heading = BlogPost.Heading,
						PageTitle = BlogPost.PageTitle,
						Content = BlogPost.Content,
						ShortDescription = BlogPost.ShortDescription,
						FeaturedImageUrl = BlogPost.FeaturedImageUrl,
						UrlHandle = BlogPost.UrlHandle,
						PublishDate = BlogPost.PublishDate,
						Author = BlogPost.Author,
						Visible = BlogPost.Visible,
						Tags = new List<Tag>(Tags.Split(',')
						.Select(x => new Tag() { Name = x.Trim() }))
					};


					await _blogPostRepository.UpdateAsync(blogPostDomainModel);

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

		// Custom validation
		private void ValidateEditBlogPost()
		{
			if (!string.IsNullOrWhiteSpace(BlogPost.Heading))
			{
				// check minLength
				if (BlogPost.Heading.Length < 8 || BlogPost.Heading.Length > 72)
				{
					ModelState.AddModelError("BlogPost.Heading",
						"Заголовок может быть от 8 до 72 символов");
				}


				// check maxLength
			}
		}
	}
}
