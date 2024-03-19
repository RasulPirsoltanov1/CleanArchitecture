using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanArchitecture.WebApi.Middlewares;

public class EndpointGroupDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // Define the base route for categories
        var routeCategoriesBase = "/api/v3/categories";

        // Ensure the path exists in the document, or create a new one
        if (!swaggerDoc.Paths.ContainsKey(routeCategoriesBase))
        {
            swaggerDoc.Paths.Add(routeCategoriesBase, new OpenApiPathItem());
        }

        // Add GET operation for retrieving all categories
        swaggerDoc.Paths[routeCategoriesBase].Operations[OperationType.Get] = new OpenApiOperation
        {
            Summary = "Get All Categories",
            Description = "Retrieves all categories.",
            Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Categories V3" } },
            Responses = new OpenApiResponses
            {
                ["200"] = new OpenApiResponse { Description = "Successful response - returns an array of categories." }
            }
        };

        // Add POST operation for creating a new category
        swaggerDoc.Paths[routeCategoriesBase].Operations[OperationType.Post] = new OpenApiOperation
        {
            Summary = "Create new Category",
            Description = "Creates a new category.",
            Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Categories V3" } },
            RequestBody = new OpenApiRequestBody
            {
                Description = "Category to create.",
                Required = true,
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["categoryName"] = new OpenApiSchema { Type = "string", Description = "Category name" },
                                ["description"] = new OpenApiSchema { Type = "string", Description = "Description" },
                            },
                            Required = new HashSet<string> { "categoryName" }
                        }
                    }
                }
            },
            Responses = new OpenApiResponses
            {
                ["201"] = new OpenApiResponse { Description = "Category created successfully." }
            }
        };
         
    }
}
