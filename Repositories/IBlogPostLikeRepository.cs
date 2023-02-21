using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories
{
	public interface IBlogPostLikeRepository
	{
		Task<int> GetTotalLikesForBlog(Guid blogPostId);
		Task AddLikeForBlog(Guid blogPostId, Guid userId);
		Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);
	}
}
