using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ImagesController : Controller
	{
		private readonly IImageRepository _imageRepository;

		public ImagesController(IImageRepository imageRepository)
		{
			_imageRepository = imageRepository;
		}


		[HttpPost]
		public async Task<IActionResult> UploadAsync(IFormFile file)
		{
			var imageUrl = await _imageRepository.UploadAsync(file);
			

			if (imageUrl == null)
			{
				return Problem("Что-то пошло не так", null, (int)HttpStatusCode.InternalServerError);
			}

			return Json(new { link = imageUrl });
		
		}
	}
}
