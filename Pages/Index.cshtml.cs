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
		private readonly ITagRepository _tagRepository;

		[BindProperty]
		public List<BlogPost> Blogs { get; set; }
		public List<Tag> Tags { get; set; }


		public IndexModel(ILogger<IndexModel> logger,
						  IBlogPostRepository blogPostRepository,
						  ITagRepository tagRepository)
		{
			_logger = logger;
            _blogPostRepository = blogPostRepository;
			_tagRepository = tagRepository;
		}

		public async Task<IActionResult> OnGet()
		{
			Blogs = (await _blogPostRepository.GetAllAsync()).ToList();
			Tags = (await _tagRepository.GetAllAsync()).ToList();

			return Page();
		}
	}
}