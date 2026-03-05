using Application.Dtos.CategoryDtos;

namespace Application.UseCases.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllAsync();
        Task<GetByIdCategoryDto> GetByIdAsync(int id);
        Task<ResultCategoryDto> CreateAsync(CreateCategoryDto model);
        Task<ResultCategoryDto> UpdateAsync(UpdateCategoryDto model);
        Task DeleteAsync(int id);
    }
}
