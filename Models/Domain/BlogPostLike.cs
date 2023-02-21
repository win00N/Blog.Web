namespace Blog.Web.Models.Domain
{
	public class BlogPostLike
	{
		public Guid BlogPostLikeId { get; set; }
		public Guid BlogPostId { get; set; }
		public Guid UserId { get; set; }
	}
}
