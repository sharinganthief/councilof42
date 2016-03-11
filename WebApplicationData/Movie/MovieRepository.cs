using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using RestSharp;
using WebApplicationData.WebApplication;

namespace WebApplicationData.Movie
{
	public class MovieRepository : BaseRepository
	{
		public override string BaseURLForFormat
		{
			get { return _baseMovieUrlForFormat; }
		}

		private readonly string _baseMovieUrlForFormat = ConfigurationManager.AppSettings["baseMovieUrlForFormat"] ??
														"http://movie.local/api/movies/{0}";
		
		public MoviesInfo GetMovieInfo()
		{
			MoviesInfo info = new MoviesInfo();
			info.Actors         = Get<List<string>>("GetAllActors").Data;
			info.Writers        = Get<List<string>>("GetAllWriters").Data;
			info.Directors      = Get<List<string>>("GetAllDirectors").Data;
			info.Genres         = Get<List<string>>("GetAllGenres").Data;
			//info.AvailableUsers = Post<List<string>>("GetAllUsersForUser", User).Data;
			info.Ratings        = Get<List<string>>("GetAllRatings").Data;
			info.Titles         = Get<List<string>>("GetAllTitles").Data;
			info.Tags           = Get<List<string>>("GetAllPlotTags").Data;
			info.Characters     = Get<List<string>>("GetAllCharacters").Data;
			info.Length         = Get<Range>("GetLengthRange").Data;
			info.Year = Get<Range>("GetYearRange").Data;

			return info;
		}

		public List<MovieResult> SearchForMovies(SearchCriteria form)
		{
			return this.Post<List<MovieResult>>("Search", form).Data;
		}

		public MovieAddResult AddMovies(List<string> moviesToAdd)
		{
			MovieAddResult result = this.Post<MovieAddResult>("Add", moviesToAdd).Data;
			if (result.Data == null) throw new ApplicationException("Error adding movies to DB");

			MovieAddRestResult restResult = result.Data;

			if (restResult.Suceeded) return result;

			if (result.Errors == null)
			{
				result.Errors = new List<string>();
			}

			foreach (string error in restResult.Errors)
			{
				result.Errors.Add(error);	
			}
			
			return result;
		}

		public List<SeenDto> GetSeensForUser(string user)
		{
			List<SeenDto> result = this.Post<List<SeenDto>>("GetSeensForUser", user).Data;
			//if (result.Data == null) throw new ApplicationException("Error adding movies to DB");

			//MovieAddRestResult restResult = result.Data;

			//if (restResult.Suceeded) return result;

			//if (result.Errors == null)
			//{
			//    result.Errors = new List<string>();
			//}

			//foreach (string error in restResult.Errors)
			//{
			//    result.Errors.Add(error);	
			//}
			
			return result;
		}

		public MovieUserAddResult RegisterMovieAPIUser(UserInfo userInfo)
		{
			MovieUserAddResult result = this.Post<MovieUserAddResult>("AddUser", new MovieUserAddRequest(){FirstName = userInfo.FirstName, LastName = userInfo.LastName}).Data;
			if (result.MovieAPIUserID == Guid.Empty) throw new ApplicationException("Error adding user to DB");

			if (result.Successful) return result;

			if (result.Errors == null)
			{
				result.Errors = new List<string>();
			}

			foreach (string error in result.Errors)
			{
				result.Errors.Add(error);	
			}
			
			return result;
		}

		public List<MovieResult> GetAllFilms()
		{
			return this.Get<List<MovieResult>>("GetAllFilms").Data;
		}

		public List<MovieResult> UpdateMoviesForUser(List<int> movieIDs, Guid movieApiUserID)
		{
			MovieUserUpdateMoviesRequest movieUserUpdateMoviesRequest = new MovieUserUpdateMoviesRequest()
			{
				Movies = movieIDs,
				UserID = movieApiUserID,
				
			};

			BaseResult<List<MovieResult>> result = this.Post<List<MovieResult>>("UpdateUserMovies", movieUserUpdateMoviesRequest);

			return result.Data;

			//if (!result.Data.Successful)
			//{

			//}


		}

		public List<MovieResult> GetAllUserFilms(Guid movieApiUserID)
		{
			BaseResult<List<MovieResult>> result = this.Post<List<MovieResult>>("GetUserMovies", movieApiUserID);

			return result.Data;
		}
	}
}
