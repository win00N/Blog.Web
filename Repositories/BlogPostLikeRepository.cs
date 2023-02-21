using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
	public class BlogPostLikeRepository : IBlogPostLikeRepository
	{
		private readonly BlogDbContext _context;

		public BlogPostLikeRepository(BlogDbContext context)
		{
			_context = context;
		}

		public async Task AddLikeForBlog(Guid blogPostId, Guid userId)
		{
			var like = new BlogPostLike
			{
				BlogPostLikeId = Guid.NewGuid(),
				BlogPostId = blogPostId,
				UserId = userId
			};

			await _context.BlogPostLike.AddAsync(like);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
		{
			return await _context.BlogPostLike
				.Where(x => x.BlogPostId == blogPostId)
				.ToListAsync();
		}

		public async Task<int> GetTotalLikesForBlog(Guid blogPostId)
		{
			return await _context.BlogPostLike
				.CountAsync(x => x.BlogPostId == blogPostId);
		}
	}
}
