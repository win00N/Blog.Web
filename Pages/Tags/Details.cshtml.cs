using Blog.Web.Models.Domain;
using Blog.Web.Pages.Admin.Blogs;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages.Tags
{
    public class DetailsModel : PageModel
    {
		private readonly IBlogPostRepository _blogPostRepository;

        public List<BlogPost> Blogs { get; set; }

        [BindProperty]
        public string TagName { get; set; }

        public DetailsModel(IBlogPostRepository blogPostRepository)
        {
			_blogPostRepository = blogPostRepository;
		}

        public async Task<IActionResult> OnGet(string tagName)
        {
			Blogs = (await _blogPostRepository.GetAllAsync(tagName)).ToList();
            TagName = tagName;
            return Page();
        }
    }
}
