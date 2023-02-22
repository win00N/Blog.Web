using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Blog.Web.Pages.Admin.Blogs
{
    [Authorize(Roles = "Admin")]
    public class AddModel : PageModel
    {
		private readonly IBlogPostRepository _blogPostRepository;

		[BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        [BindProperty]
		[Required]
        public string Tags { get; set; }

        public AddModel(IBlogPostRepository blogPostRepository)
        {
			_blogPostRepository = blogPostRepository;
		}

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost() 
        {
			ValidateAddBlogPost();

			if (ModelState.IsValid)
            {
				var blogPost = new BlogPost()
				{
					Heading = AddBlogPostRequest.Heading,
					PageTitle = AddBlogPostRequest.PageTitle,
					Content = AddBlogPostRequest.Content,
					ShortDescription = AddBlogPostRequest.ShortDescription,
					FeaturedImageUrl = AddBlogPostRequest.FeaturedImageUrl,
					UrlHandle = AddBlogPostRequest.UrlHandle,
					PublishDate = AddBlogPostRequest.PublishDate,
					Author = AddBlogPostRequest.Author,
					Visible = AddBlogPostRequest.Visible,
					Tags = new List<Tag>(Tags.Split(',')
				.Select(x => new Tag() { Name = x.Trim() }))
				};

				await _blogPostRepository.AddAsync(blogPost);

				var notification = new Notification
				{
					Message = "Новый пост успешно создан",
					Type = Enums.NotificationType.Success,
				};

				TempData["Notification"] = JsonSerializer.Serialize(notification);

				return RedirectToPage("/Admin/Blogs/List");
			}

			return Page();
        }

		private void ValidateAddBlogPost()
		{
			if (AddBlogPostRequest.PublishDate.Date < DateTime.Now.Date)
			{
				ModelState.AddModelError("AddBlogPostRequest.PublishDate",
					"Дата публикации может быть только сегодняшняя или в будущем");
			}
		}

    }
}
