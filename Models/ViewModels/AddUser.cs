using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.ViewModels
{
	public class AddUser
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(4)]
		public string Password { get; set; }
		public bool AdminCheckbox { get; set; }
	}
}
