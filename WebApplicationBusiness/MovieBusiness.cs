using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using RestSharp;
using WebApplicationData;
using WebApplicationData.Movie;
using WebApplicationData.WebApplication;

namespace WebApplicationBusiness
{
	public class MovieBusiness
	{

		private MovieRepository movieRepo = new MovieRepository();

		public MoviesInfo GetMoviesInfo()
		{
			MoviesInfo info = Cache.Get<MoviesInfo>("MovieInfo");
			if (info != null) return info;

			info = movieRepo.GetMovieInfo();
			Cache.Add(info, "MovieInfo", 1440);
			return GetMoviesInfo();
		}

		public List<MovieResult> SearchForMovies(SearchCriteria form)
		{
			//List<MovieResult> movies = Cache.Get<List<MovieResult>>(form.GetHashCode().ToString());
			//if (movies != null) return movies;

			List<MovieResult> movies = movieRepo.SearchForMovies(form);
			return movies;
			//Cache.Add(movies, form.GetHashCode().ToString(), expirationTime: 10 );
			//return SearchForMovies(form);

		}

		public MovieAddResult AddMovies(List<string> moviesToAdd)
		{
			return movieRepo.AddMovies(moviesToAdd);
		}

		public List<SeenDto> GetSeensForUser(string user)
		{
			return movieRepo.GetSeensForUser(user);
		}

		public MovieUserAddResult RegisterMovieAPIUser(UserInfo userInfo)
		{
			return movieRepo.RegisterMovieAPIUser(userInfo);
		}

		public List<MovieResult> GetAllFilms()
		{
			return movieRepo.GetAllFilms();
		}

		public object UpdateMoviesForUser(List<int> movieIDs, Guid movieApiUserID)
		{
			return movieRepo.UpdateMoviesForUser(movieIDs, movieApiUserID);
		}

		public List<MovieResult> GetAllUserFilms(Guid movieApiUserID)
		{
			return movieRepo.GetAllUserFilms(movieApiUserID);
		}
	}
}
