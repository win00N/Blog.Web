using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Blog.Web.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
		private readonly IBlogPostRepository _blogPostRepository;

		public List<BlogPost> BlogPosts { get; set; }

        public ListModel(IBlogPostRepository blogPostRepository)
        {
			_blogPostRepository = blogPostRepository;
		}

        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"]; 

            if (notificationJson != null)
            {
                ViewData["Notification"] = 
                    JsonSerializer.Deserialize<Notification>(notificationJson);
            }


            BlogPosts = (await _blogPostRepository.GetAllAsync())?.ToList();
		}
    }
}
