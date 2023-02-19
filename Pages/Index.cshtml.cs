using Blog.Web.Models.Domain;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
        private readonly IBlogPostRepository _blogPostRepository;

		[BindProperty]
		public List<BlogPost> Blogs { get; set; }


		public IndexModel(ILogger<IndexModel> logger,
						  IBlogPostRepository blogPostRepository)
		{
			_logger = logger;
            _blogPostRepository = blogPostRepository;
        }

		public async Task<IActionResult> OnGet()
		{
			Blogs = (await _blogPostRepository.GetAllAsync()).ToList();
			return Page();
		}
	}
}