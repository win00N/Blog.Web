using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
	public class BlogPostCommentRepository : IBlogPostCommentRepository
	{
		private readonly BlogDbContext _context;

		public BlogPostCommentRepository(BlogDbContext context)
		{
			_context = context;
		}


		public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
		{
			await _context.BlogPostComment.AddAsync(blogPostComment);
			await _context.SaveChangesAsync();
			return blogPostComment;
		}

		public async Task<IEnumerable<BlogPostComment>> GetAllAsync(Guid blogPostId)
		{
			return await _context.BlogPostComment
				.Where(x => x.BlogPostId == blogPostId)
				.ToListAsync();
		}
	}
}
