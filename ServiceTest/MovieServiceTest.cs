using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using FakeItEasy;
using Service;
using Service.Specifications;
using ServiceAbstraction;
using Shared.Dtos;

namespace ServiceTest;

public class MovieServiceTest
{
    private readonly IMovieService _movieService;
    private readonly IGenreService _genreService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MovieServiceTest()
    {
        _mapper = A.Fake<IMapper>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _genreService = A.Fake<IGenreService>();
        _movieService = new MovieService(_unitOfWork, _mapper, _genreService);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnPaginatedResult()
    {
        // Arrange
        var parSpec = new MovieParameterSpecification
        {
            PageIndex = 1,
            PageSize = 10
        };
        var fakeMovie = MovieFactory.CreateFakeMovie();
        var fakeRepo = A.Fake<IGenericRepo<Movie, Guid>>();
        A.CallTo(() => fakeRepo.GetAllAsync(A<MovieSpecifications>.Ignored)).Returns(fakeMovie);
        A.CallTo(() => _unitOfWork.GetRepo<Movie, Guid>()).Returns(fakeRepo);
        var mappedMovies = new List<ResponseMovieScheduleDto>()
        {
            new ResponseMovieScheduleDto()
            {
                Id = fakeMovie[0].Id,
                Name = fakeMovie[0].Name,
                Description = fakeMovie[0].Description,
                DurationMinutes = fakeMovie[0].DurationMinutes,
                Genres = fakeMovie[0].Genres.Select(g => g.Name).ToList(),
                Rating = fakeMovie[0].Rating,
                ReleaseDate = fakeMovie[0].ReleaseDate,
                Schedules = fakeMovie[0].Schedules
            },
            new ResponseMovieScheduleDto()
            {
                Id = fakeMovie[1].Id,
                Name = fakeMovie[1].Name,
                Description = fakeMovie[1].Description,
                DurationMinutes = fakeMovie[1].DurationMinutes,
                Genres = fakeMovie[1].Genres.Select(g => g.Name).ToList(),
                Rating = fakeMovie[1].Rating,
                ReleaseDate = fakeMovie[1].ReleaseDate,
                Schedules = fakeMovie[1].Schedules
            }
        };
        A.CallTo(() => _mapper.Map<IEnumerable<ResponseMovieScheduleDto>>(fakeMovie)).Returns(mappedMovies);
        // Act
        var res = await _movieService.GetAllAsync(parSpec);
        var lst = res.Data.ToList();
        // Assert
        Assert.NotNull(res);
        Assert.Equal("Movie 1", lst[0].Name);
        Assert.Equal(new DateTime(2000, 1, 1), lst[0].ReleaseDate);
        Assert.Equal(new DateTime(2024, 1, 2, 11, 0, 0), lst[1].Schedules.Skip(1).First().ShowDateTime);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnMovie()
    {
        // Arrange
        var fakeMovie = MovieFactory.CreateFakeMovie();
        var movieId = fakeMovie[0].Id;
        var fakeRepo = A.Fake<IGenericRepo<Movie, Guid>>();
        A.CallTo(() => fakeRepo.GetByIdAsync(A<MovieSpecifications>.Ignored)).Returns(fakeMovie[0]);
        A.CallTo(() => _unitOfWork.GetRepo<Movie, Guid>()).Returns(fakeRepo);
        A.CallTo(() => _unitOfWork.ScheduleRepo.GetSchedulesByMovieIdAsync(movieId))
            .Returns(fakeMovie[0].Schedules);
        var mappedMovie = new ResponseMovieScheduleDto()
        {
            Id = fakeMovie[0].Id,
            Name = fakeMovie[0].Name,
            Description = fakeMovie[0].Description,
            DurationMinutes = fakeMovie[0].DurationMinutes,
            Genres = fakeMovie[0].Genres.Select(g => g.Name).ToList(),
            Rating = fakeMovie[0].Rating,
            ReleaseDate = fakeMovie[0].ReleaseDate,
            Schedules = fakeMovie[0].Schedules
        };
        A.CallTo(() => _mapper.Map<ResponseMovieScheduleDto>(fakeMovie[0])).Returns(mappedMovie);
        // Act
        var res = await _movieService.GetByIdAsync(movieId);
        // Assert
        Assert.NotNull(res);
        Assert.Equal("Movie 1", res.Name);
        Assert.Equal(new DateTime(2000, 1, 1), res.ReleaseDate);
        Assert.Equal(new DateTime(2024, 1, 2, 11, 0, 0), res.Schedules.Skip(1).First().ShowDateTime);
    }

}