namespace Blog.Web.Models.Domain
{
	public class Tag
	{
		public Guid TagId { get; set; }
		public string Name { get; set; }
		public Guid BlogPostId { get; set; }
	}
}
