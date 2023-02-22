using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.ViewModels
{
	public class Register
	{
		[Required]
		public string Login { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(4)]
		public string Password { get; set; }
	}
}
