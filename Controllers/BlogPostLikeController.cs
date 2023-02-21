using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BlogPostLikeController : Controller
	{
		private readonly IBlogPostLikeRepository _blogPostLikeRepository;

		public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
		{
			_blogPostLikeRepository = blogPostLikeRepository;
		}

		[Route("Add")]
		[HttpPost]
		public async Task<IActionResult> AddLike(
			[FromBody] AddBlogPostLikeRequest addBlogPostLikeRequest)
		{
			await _blogPostLikeRepository.AddLikeForBlog
				(addBlogPostLikeRequest.BlogPostId,
				addBlogPostLikeRequest.UserId);

			return Ok();
		}

		[HttpGet]
		[Route("{blogpostid:Guid}/totalLikes")]
		public async Task<IActionResult> GetTotalLikes([FromRoute] Guid blogPostId)
		{
			var totalLikes = await _blogPostLikeRepository.GetTotalLikesForBlog(blogPostId);

			return Ok(totalLikes);
		}
	}
}
