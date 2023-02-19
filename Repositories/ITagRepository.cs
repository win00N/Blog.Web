using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories
{
	public interface ITagRepository
	{
		Task<IEnumerable<Tag>> GetAllAsync();
	}
}
