using Blog.Web.Models.Domain;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages.SingleBlog
{
	public class DetailsModel : PageModel
	{
		private readonly IBlogPostRepository _blogPostRepository;

		public BlogPost BlogPost { get; set; }

		public DetailsModel(IBlogPostRepository blogPostRepository)
		{
			_blogPostRepository = blogPostRepository;
		}

		public async Task<IActionResult> OnGet(string urlHandle)
		{
			BlogPost = await _blogPostRepository.GetAsync(urlHandle);
			return Page();
		}
	}
}
