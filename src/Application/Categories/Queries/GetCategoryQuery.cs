namespace CleanArchitecture.Application.Categories.Queries;

public record GetCategoryQuery(int Id) : IRequest<Category>;
public class GetCategoryQueryHandler(ICategoryRepository _categoryRepository) : IRequestHandler<GetCategoryQuery, Category>
{
    public async Task<Category> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetByIdAsync(request.Id);
    }
}