using Application.Dtos.CategoryDtos;
using Application.Interfaces;
using Domain.Entities;
using System.Reflection;

namespace Application.UseCases.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        public CategoryService(IBaseRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<ResultCategoryDto>> GetAllAsync()
        {
           var categories = await _categoryRepository.GetAllAsync();
            return categories.Select( c => new ResultCategoryDto
            {
                CategoryId = c.Id,
                CategoryName = c.CategoryName,
                ModifiedDate = c.ModifiedDate,
                CreatedDate = c.CreatedDate,
                IsActive = c.IsActive,
                IsDeleted = c.IsDeleted
            }).ToList();
        }

        public async Task<GetByIdCategoryDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var result = new GetByIdCategoryDto() 
            {
                CategoryId = category.Id,   
                CategoryName = category.CategoryName,
                ModifiedDate = category.ModifiedDate,
                CreatedDate = category.CreatedDate,
                IsActive = category.IsActive,
                IsDeleted = category.IsDeleted
            };

            return result;
        }

        public async Task<ResultCategoryDto> CreateAsync(CreateCategoryDto model)
        {
            Category category = new Category()
            {
                CategoryName = model.CategoryName,
                IsActive = model.IsActive,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            var addedCategory = await _categoryRepository.CreateAsync(category);

            return new ResultCategoryDto()
            {
                CategoryId = addedCategory.Id,
                CategoryName = addedCategory.CategoryName,
                ModifiedDate = addedCategory.ModifiedDate,
                CreatedDate = addedCategory.CreatedDate,
                IsActive = addedCategory.IsActive,
                IsDeleted = addedCategory.IsDeleted
            };
        }

        public async Task<ResultCategoryDto> UpdateAsync(UpdateCategoryDto model)
        {
            Category category = await _categoryRepository.GetByIdAsync(model.CategoryId);
            category.CategoryName = model.CategoryName;
            category.ModifiedDate = DateTime.UtcNow;
            category.IsActive = model.IsActive;
            category.IsDeleted = model.IsDeleted;

            var updatedCategory = await _categoryRepository.UpdateAsync(category);
            return new ResultCategoryDto()
            {
                CategoryId = updatedCategory.Id,
                CategoryName = updatedCategory.CategoryName,
                ModifiedDate = updatedCategory.ModifiedDate,
                CreatedDate = updatedCategory.CreatedDate,
                IsActive = updatedCategory.IsActive,
                IsDeleted = updatedCategory.IsDeleted
            };
        }

        public async Task DeleteAsync(int id)
        {
            Category deletedCategory = await _categoryRepository.GetByIdAsync(id);

            await _categoryRepository.DeleteAsync(deletedCategory);
        }
    }
}
