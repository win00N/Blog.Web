
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
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
		private readonly IBlogPostCommentRepository _blogPostCommentRepository;

		public BlogPost BlogPost { get; set; }
		public List<BlogComment> Comments { get; set; }
		public int TotalLikes { get; set; }
		public bool Liked { get; set; }

		[BindProperty]
		public Guid BlogPostId { get; set; }

		[BindProperty]
		public string CommentDescription { get; set; }



		public DetailsModel(IBlogPostRepository blogPostRepository,
							IBlogPostLikeRepository blogPostLikeRepository,
							SignInManager<IdentityUser> signInManager,
							UserManager<IdentityUser> userManager,
							IBlogPostCommentRepository blogPostCommentRepository)
		{
			_blogPostRepository = blogPostRepository;
			_blogPostLikeRepository = blogPostLikeRepository;
			_signInManager = signInManager;
			_userManager = userManager;
			_blogPostCommentRepository = blogPostCommentRepository;
		}

		public async Task<IActionResult> OnGet(string urlHandle)
		{
			BlogPost = await _blogPostRepository.GetAsync(urlHandle);

			if (BlogPost != null)
			{
				BlogPostId = BlogPost.BlogPostId;
				if (_signInManager.IsSignedIn(User))
				{
					var likes = await _blogPostLikeRepository.GetLikesForBlog(BlogPost.BlogPostId);

					var userId = _userManager.GetUserId(User);

					Liked = likes.Any(x => x.UserId == Guid.Parse(userId));
					
					await GetComments();

				}


				TotalLikes = await _blogPostLikeRepository
					.GetTotalLikesForBlog(BlogPost.BlogPostId);
			}

			return Page();
		}


		public async Task<IActionResult> OnPost(string urlHandle)
		{
			if (_signInManager.IsSignedIn(User) 
				&& !string.IsNullOrWhiteSpace(CommentDescription))
			{
				var userId = _userManager.GetUserId(User);

				var comment = new BlogPostComment
				{
					BlogPostId = this.BlogPostId,
					Description = CommentDescription,
					DateAdded = DateTime.Now,
					UserId = Guid.Parse(userId)
				};

				await _blogPostCommentRepository.AddAsync(comment);
			}

			return RedirectToPage("/SingleBlog/Details", new { urlHandle = urlHandle });
		}

		private async Task GetComments()
		{
			var blogPostComments = await _blogPostCommentRepository.GetAllAsync(BlogPost.BlogPostId);

			var blogCommentsViewModel = new List<BlogComment>();

			foreach (var blogPostComment in blogPostComments)
			{
				blogCommentsViewModel.Add( new BlogComment 
				{
					DateAdded = blogPostComment.DateAdded,
					Description = blogPostComment.Description,
					UserName = (await _userManager.FindByIdAsync(blogPostComment.UserId.ToString())).UserName,
				});
			}

			Comments = blogCommentsViewModel;
		}
	}
}
