using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using RestSharp;
using WebApplicationData;
using WebApplicationData.Image;

namespace WebApplicationBusiness
{
	public class ImageBusiness
	{
		private const string imageCacheKey = "ImageResults";
		private const string imageTagCacheKey = "ImageTags";
		private readonly int recordsPerPage;

		public ImageBusiness()
		{
			this.recordsPerPage = Settings.ImagePerPage;
		}
		
		
		private ImageRepository imageRepo = new ImageRepository();

		public List<ImageDto> GetAllImages(int page, bool bypassCache = false)
		{

			List<ImageDto> imageResults = null;

			if (!bypassCache)
			{
				imageResults = Cache.Get<List<ImageDto>>(imageCacheKey);
			}
			
			if (imageResults != null) return imageResults.GetPaginated(page, recordsPerPage);

			imageResults = imageRepo.GetAllImages();
			Cache.Add(imageResults, imageCacheKey, 60);
			return GetAllImages(page);

			
		}

		public AddImageResult UploadImage(string url, string name, string description, bool nsfw, List<string> tags )
		{
			AddImageResult result = imageRepo.UploadImage(url, name, description, nsfw, tags);

			if (result.Success)
			{
				ExpireImageCache();
			}

			return result;
		}

		private void ExpireImageCache()
		{
			GetAllImages(0, true);
		}

		public List<ImageDto> GetPaginatedImages(int page, string[] tags = null)
		{
			List<ImageDto> images = null;

			images = tags != null ? FilterByTags(tags.ToList(), page) : GetAllImages(page);

			return images;

		}

		

		public List<string> GetAllTags(bool bypassCache = false)
		{
			List<string> tags = null;

			if (!bypassCache)
			{
				tags = Cache.Get<List<string>>(imageTagCacheKey);
			}
			
			if (tags != null) return tags;

			tags = this.imageRepo.GetAllTags();
			Cache.Add(tags, imageTagCacheKey, 60);
			return GetAllTags();


		}

		public List<ImageDto> FilterByTags(List<string> tags, int? page)
		{

			List<ImageDto> images = null;

			string tagsCollectionKey = String.Join("-", tags);
			
			images = Cache.Get<List<ImageDto>>(tagsCollectionKey);


			if (images != null)
			{
				return page != null ? images.GetPaginated((int) page, recordsPerPage) : images;
			}

			images = this.imageRepo.FilterByTags(tags);
			Cache.Add(images, tagsCollectionKey, 20);
			return FilterByTags(tags, page);
		}

	    public Dictionary<string, int> GetImageTagCounts()
	    {
	        return this.imageRepo.GetImageTagCounts();
	    }
	}
}
