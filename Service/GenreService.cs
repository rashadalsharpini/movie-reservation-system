using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.Dtos;

namespace Service;

public class GenreService(IUnitOfWork unitOfWork,IMapper mapper ):IGenreService
{
    public async Task<PaginatedResult<ResponseGenreDto>> GetAllAsync(MovieParameterSpecification parameterSpecification)
    {
        var geners = await unitOfWork.GetRepo<Genre, Guid>().GetAllAsync(new GenreSpecification(parameterSpecification));
        var result1= mapper.Map<IEnumerable<ResponseGenreDto>>(geners);
        var finalResult = new PaginatedResult<ResponseGenreDto>(
            parameterSpecification.PageIndex,
            parameterSpecification.PageSize,
            null,
            result1
        );
        return finalResult;
        
    }
    
    public async Task<Genre> GetOrCreateAsync(string genre)
    {
        var correctedName= genre.ToLower().Trim();
        var existingGenre = await unitOfWork.GetRepo<Genre, Guid>().FindByNameAsync(correctedName);
        if(existingGenre is not null)return existingGenre;
        var newGenre =new Genre { Id = Guid.NewGuid(),Name = correctedName };
        await unitOfWork.GetRepo<Genre, Guid>().AddAsync(newGenre);
        await unitOfWork.SaveChangesAsync();
        return newGenre;
    }

    public async Task<ResponseGenreDto> CreateGenreAsync(CreateOrUpdateGenreDto genreDto)
    {
        var newMovie= mapper.Map<Genre>(genreDto);
        await unitOfWork.GetRepo<Genre,Guid>().AddAsync(newMovie);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<ResponseGenreDto>(newMovie);
    }

    public async Task<bool> UpdateGenreAsync(Guid id, CreateOrUpdateGenreDto genreDto)
    {
        var existingGenre = await unitOfWork.GetRepo<Genre, Guid>().GetByIdAsync(id);
        if (existingGenre == null) throw new Exception($"Genre with id {id} was not found"); 
        existingGenre= mapper.Map(genreDto, existingGenre);
        unitOfWork.GetRepo<Genre,Guid>().Update(existingGenre);
        return await unitOfWork.SaveChangesAsync()>0;
    }

    public async Task<bool> DeleteGenreAsync(Guid id)
    {
        var existingGenre= await unitOfWork.GetRepo<Genre, Guid>().GetByIdAsync(id);
        if (existingGenre == null) throw new Exception($"Genre with id {id} was not found");
        unitOfWork.GetRepo<Genre,Guid>().Delete(existingGenre);
        return await unitOfWork.SaveChangesAsync()>0;
    }
    
}