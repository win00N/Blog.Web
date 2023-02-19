using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
	public class TagRepository : ITagRepository
	{
		private readonly BlogDbContext _context;

		public TagRepository(BlogDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Tag>> GetAllAsync()
		{
			var tags = await _context.Tags.ToListAsync();
			return tags.DistinctBy(x => x.Name.ToLower());
		}
	}
}
