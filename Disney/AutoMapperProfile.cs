using AutoMapper;
using Disney.DTO.Character;
using Disney.DTO.Genre;
using Disney.DTO.Movie;
using Disney.DTO.CharacterMovie;
using Disney.Models;

namespace Disney
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Character
            CreateMap<Character, GetCharacterDTO>();
            CreateMap<AddCharacterDTO, Character>();
            CreateMap<Character, GetCharacterWithMoviesDTO>();

            //Movie
            CreateMap<Movie, GetMovieDTO>();
            CreateMap<Movie, GetMovieWithDetailsDTO>();
            CreateMap<AddMovieDTO, Movie>();
            CreateMap<UpdateMovieDTO, Movie>();


            

            //Genre
            CreateMap<Genre, GetGenreDTO>();
            CreateMap<AddGenreDTO, Genre>();
            CreateMap<UpdateGenreDTO, Genre>();

        }
    }
}
