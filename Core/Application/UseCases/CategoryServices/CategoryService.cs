using Application.Dtos.CategoryDtos;
using Application.Interfaces;
using Domain.Entities;
using System.Reflection;

namespace Application.UseCases.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<ResultCategoryDto>> GetAllAsync()
        {
           var categories = await _categoryRepository.GetAllAsync();
            return categories.Select( c => new ResultCategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }).ToList();
        }

        public async Task<GetByIdCategoryDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var result = new GetByIdCategoryDto() 
            {
                CategoryId = category.CategoryId,   
                CategoryName = category.CategoryName
            };

            return result;
        }

        public async Task<ResultCategoryDto> CreateAsync(CreateCategoryDto model)
        {
           Category category = new Category()
           {
                CategoryName = model.CategoryName
           };

            var addedCategory = await _categoryRepository.CreateAsync(category);

            return new ResultCategoryDto()
            {
                CategoryId = addedCategory.CategoryId,
                CategoryName = addedCategory.CategoryName,
            };
        }

        public async Task<ResultCategoryDto> UpdateAsync(UpdateCategoryDto model)
        {
            Category category = await _categoryRepository.GetByIdAsync(model.CategoryId);
            category.CategoryName = model.CategoryName;

            var updatedCategory = await _categoryRepository.UpdateAsync(category);
            return new ResultCategoryDto()
            {
                CategoryId = updatedCategory.CategoryId,
                CategoryName = updatedCategory.CategoryName,
            };
        }

        public async Task DeleteAsync(int id)
        {
            Category deletedCategory = await _categoryRepository.GetByIdAsync(id);

            await _categoryRepository.DeleteAsync(deletedCategory);
        }
    }
}
