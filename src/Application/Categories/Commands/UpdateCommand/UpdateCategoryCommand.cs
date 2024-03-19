namespace CleanArchitecture.Application.Categories.Commands.UpdateCommand;

public class UpdateCategoryCommand : IRequest<Category>
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string? Description { get; set; }
}

public class UpdateCategoryCommandHandler(ICategoryRepository _categoryRepository)
    : IRequestHandler<UpdateCategoryCommand, Category>
{
    public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);

        category.CategoryName = request.CategoryName;
        category.Description = request.Description;

        var response = await _categoryRepository.UpdateAsync(category); 

        return response;
    }
}

