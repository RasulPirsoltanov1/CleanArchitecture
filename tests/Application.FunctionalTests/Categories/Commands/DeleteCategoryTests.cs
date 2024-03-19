using CleanArchitecture.Application.Categories.Commands.CreateCommand;
using CleanArchitecture.Application.Categories.Commands.DeleteCommand;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;

namespace Application.FunctionalTests.Categories.Commands;

public class DeleteCategoryTests : BaseTestFixture
{

    [Test]
    public async Task ShouldDeleteCategory()
    {
        var categoryId = await SendAsync(new CreateCategoryCommand()
        {
            CategoryName = "Beverages",
            Description = "Soft, Tea"
        });

        await SendAsync(new DeleteCategoryCommand(categoryId));
        var category = await FindAsync<Category>(categoryId);

        category.Should().BeNull(); 
    }
}
