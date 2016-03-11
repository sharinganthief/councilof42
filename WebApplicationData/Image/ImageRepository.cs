using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationData.Comic;

namespace WebApplicationData.Image
{
	public class ImageRepository : BaseRepository
	{
		
		public override string BaseURLForFormat
		{
			get { return _baseImageCollectionUrlForFormat; }
		}

		private readonly string _baseImageCollectionUrlForFormat = Settings.ImageCollectionBaseUrlForFormat ?? "http://image.local/api/values/{0}";

		public AddImageResult UploadImage(string url, string name, string description, bool nsfw, List<string> tags )
		{

			string imageUploadUrl = "AddImage";

			ImageDto dto = new ImageDto()
			{
				ImageDescription = description,
				ImageName = name,
				ImageURL = url,
				NSFW = nsfw,
				Tags = tags,

			};

			AddImageResult result = this.Post<AddImageResult>(imageUploadUrl, dto).Data;

			return result;
		}

		public List<ImageDto> GetAllImages()
		{
			string imageGetUrl = "Get";

			List<ImageDto> result = this.Get<List<ImageDto>>(imageGetUrl).Data;

			return result;
		}

		public List<string> GetAllTags()
		{
			string imageGetUrl = "GetAllTags";

			List<string> result = this.Get<List<string>>(imageGetUrl).Data;

			return result;
		}

		public List<ImageDto> FilterByTags(List<string> tags )
		{
			string imageFilterUrl = "FilterByTags";

			List<ImageDto> result = this.Post<List<ImageDto>>(imageFilterUrl, tags).Data;

			return result;
		}

		public Dictionary<string, int> GetImageTagCounts()
		{
			string imageTagCountUrl = "ImageTagCounts";

			Dictionary<string, int> result = this.Get<Dictionary<string, int>>(imageTagCountUrl).Data;

			return result;

		}
	}
}
