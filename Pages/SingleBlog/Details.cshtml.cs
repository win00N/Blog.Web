
using Blog.Web.Models.Domain;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages.SingleBlog
{
	public class DetailsModel : PageModel
	{
		private readonly IBlogPostRepository _blogPostRepository;
		private readonly IBlogPostLikeRepository _blogPostLikeRepository;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public BlogPost BlogPost { get; set; }
		public int TotalLikes { get; set; }
		public bool Liked { get; set; }

		public DetailsModel(IBlogPostRepository blogPostRepository,
							IBlogPostLikeRepository blogPostLikeRepository,
							SignInManager<IdentityUser> signInManager,
							UserManager<IdentityUser> userManager)
		{
			_blogPostRepository = blogPostRepository;
			_blogPostLikeRepository = blogPostLikeRepository;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public async Task<IActionResult> OnGet(string urlHandle)
		{
			BlogPost = await _blogPostRepository.GetAsync(urlHandle);

			if (BlogPost != null)
			{
				if (_signInManager.IsSignedIn(User))
				{
					var likes = await _blogPostLikeRepository.GetLikesForBlog(BlogPost.BlogPostId);

					var userId = _userManager.GetUserId(User);

					Liked = likes.Any(x => x.UserId == Guid.Parse(userId));
				}


				TotalLikes = await _blogPostLikeRepository
					.GetTotalLikesForBlog(BlogPost.BlogPostId);
			}

			return Page();
		}
	}
}
