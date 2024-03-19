using CleanArchitecture.Application.Categories.Commands.CreateCommand;
using CleanArchitecture.Application.Categories.Commands.UpdateCommand;
using FluentAssertions;

namespace Application.FunctionalTests.Categories.Commands;
public class UpdateCategoryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldUpdateCategory()
    {
        var command = new CreateCategoryCommand
        {
            CategoryName = "Beverages",
            Description = "Soft, Tea",
        };

        var id = await SendAsync(command);

        var updateCommand = new UpdateCategoryCommand
        {
            Id = id,
            CategoryName = "New Beverages",
            Description = "New Soft, Tea"
        };

        var entity = await SendAsync(updateCommand);

        entity.Should().NotBeNull();
        entity.CategoryName.Should().Be(updateCommand.CategoryName);
        entity.Description.Should().Be(updateCommand.Description); 
    }
}