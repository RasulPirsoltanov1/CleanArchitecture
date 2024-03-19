using CleanArchitecture.Application.Categories.Commands.CreateCommand;
using CleanArchitecture.Application.Categories.Commands.DeleteCommand;
using CleanArchitecture.Application.Categories.Commands.UpdateCommand;
using CleanArchitecture.Application.Categories.Queries;

namespace CleanArchitecture.WebApi.Endpoints.v3;

/// <summary>
/// Defines the endpoint mappings for category operations.
/// </summary>
public class Categories : EndpointGroupBase
{
    /// <summary>
    /// Maps category-related endpoints to the application.
    /// </summary>
    /// <param name="app">The application to map the endpoints to.</param>
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .MapPost(CreateCategory)
           .MapPut(UpdateCategory, "{id}")
           .MapGet(GetCategories, "")
           .MapGet(GetCategory, "{id}")
           .MapDelete(DeleteCategory, "{id}");
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="sender">The mediator to handle the command.</param>
    /// <param name="command">The command to create a new category.</param>
    /// <returns>A result indicating the outcome of the operation.</returns>
    public async Task<IResult> CreateCategory(ISender sender, CreateCategoryCommand command)
    {
        var categoryId = await sender.Send(command);
        return Results.Created($"/categories/{categoryId}", categoryId);
    }

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="sender">The mediator to handle the command.</param>
    /// <param name="command">The command to update the category.</param>
    /// <returns>A result indicating the outcome of the operation.</returns>
    public async Task<IResult> UpdateCategory(ISender sender, UpdateCategoryCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }

    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <param name="sender">The mediator to handle the query.</param>
    /// <returns>A result containing the list of categories.</returns>
    public async Task<IResult> GetCategories(ISender sender)
    {
        var categories = await sender.Send(new GetCategoriesQuery());
        return Results.Ok(categories);
    }

    /// <summary>
    /// Retrieves a single category by its ID.
    /// </summary>
    /// <param name="sender">The mediator to handle the query.</param>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <returns>A result containing the category if found; otherwise, a not found result.</returns>
    public async Task<IResult> GetCategory(ISender sender, int id)
    {
        var category = await sender.Send(new GetCategoryQuery(id));
        if (category == null) return Results.NotFound();
        return Results.Ok(category);
    }

    /// <summary>
    /// Deletes a category by its ID.
    /// </summary>
    /// <param name="sender">The mediator to handle the command.</param>
    /// <param name="id">The ID of the category to delete.</param>
    /// <returns>A result indicating the outcome of the operation.</returns>
    public async Task<IResult> DeleteCategory(ISender sender, int id)
    {
        await sender.Send(new DeleteCategoryCommand(id));
        return Results.NoContent();
    }
}
