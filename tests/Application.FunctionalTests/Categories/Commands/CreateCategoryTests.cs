using CleanArchitecture.Application.Categories.Commands.CreateCommand;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
 
namespace Application.FunctionalTests.Categories.Commands;

public class CreateCategoryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateCategory()
    {
        var command = new CreateCategoryCommand
        {
            CategoryName = "Beverages",
            Description = "Soft, Tea"
        };

        var categoryId = await SendAsync(command);
        var category = await FindAsync<Category>(categoryId);

        category.Should().NotBeNull();
        category.CategoryName.Should().Be(command.CategoryName);
        category.Description.Should().Be(command.Description);
    }
}
