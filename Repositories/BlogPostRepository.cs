using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
	public class BlogPostRepository : IBlogPostRepository
	{
		private readonly BlogDbContext _context;

		public BlogPostRepository(BlogDbContext context)
		{
			_context = context;
		}

		public async Task<BlogPost> AddAsync(BlogPost blogPost)
		{
			await _context.BlogPosts.AddAsync(blogPost);
			await _context.SaveChangesAsync();
			return blogPost;
		}


		public async Task<bool> DeleteAsync(Guid id)
		{
			var existingBlogPost = await _context.BlogPosts.FindAsync(id);

			if (existingBlogPost != null)
			{
				_context.BlogPosts.Remove(existingBlogPost);
				await _context.SaveChangesAsync();
				return true;
			}

			return false;
		}

		public async Task<IEnumerable<BlogPost>> GetAllAsync()
		{
			return await _context.BlogPosts.ToListAsync();
		}

		public async Task<BlogPost> GetAsync(Guid id)
		{
			return await _context.BlogPosts.FindAsync(id);
		}
		public async Task<BlogPost> GetAsync(string urlHandle)
		{
			return await _context.BlogPosts.FirstOrDefaultAsync(
				x => x.UrlHandle == urlHandle);
		}

		public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
		{
			var existingBlogPost = await _context.BlogPosts.FindAsync(blogPost.BlogPostId);

			if (existingBlogPost != null)
			{
				existingBlogPost.Heading = blogPost.Heading;
				existingBlogPost.PageTitle = blogPost.PageTitle;
				existingBlogPost.Content = blogPost.Content;
				existingBlogPost.ShortDescription = blogPost.ShortDescription;
				existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
				existingBlogPost.UrlHandle = blogPost.UrlHandle;
				existingBlogPost.PublishDate = blogPost.PublishDate;
				existingBlogPost.Author = blogPost.Author;
				existingBlogPost.Visible = blogPost.Visible;

			}

			await _context.SaveChangesAsync();
			return existingBlogPost;
		}
	}
}
