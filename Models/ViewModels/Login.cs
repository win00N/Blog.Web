using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.ViewModels
{
	public class Login
	{
		[Required]
		public string Username { get; set; }
		
		[Required]
		[MinLength(4)]
		public string Password { get; set; }
	}
}
