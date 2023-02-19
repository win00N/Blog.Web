using CloudinaryDotNet;

namespace Blog.Web.Repositories
{
	public class ImageRepositoryCloudinary : IImageRepository
	{
		private readonly Account account;

		public ImageRepositoryCloudinary(IConfiguration configuration)
		{
			account = new Account
			{
				Cloud = configuration.GetSection("Cloudinary")["CloudName"],
				ApiKey = configuration.GetSection("Cloudinary")["ApiKey"],
				ApiSecret = configuration.GetSection("Cloudinary")["ApiSecret"],
			};
		}

		public async Task<string> UploadAsync(IFormFile file)
		{
			var client = new Cloudinary(account);
			var uploadFileResult = await client.UploadAsync(
				new CloudinaryDotNet.Actions.ImageUploadParams()
				{
					File = new FileDescription(file.FileName, file.OpenReadStream()),
					DisplayName = file.FileName
				});

			if (uploadFileResult != null && uploadFileResult.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return uploadFileResult.SecureUri.ToString();
			}

			return null;
		}
	}
}
