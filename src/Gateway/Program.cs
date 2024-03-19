using Microsoft.OpenApi.Models;
using MMLib.Ocelot.Provider.AppConfiguration;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

const string routesConfigFolder = "Routes";
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddOcelotWithSwaggerSupport(options => options.Folder = routesConfigFolder)
    .AddEnvironmentVariables();


builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddSwaggerForOcelot(builder.Configuration,
  (o) =>
  {
      o.GenerateDocsDocsForGatewayItSelf(opt =>
      {
          //opt.FilePathsForXmlComments = { "MyAPI.xml" };
          opt.GatewayDocsTitle = "My Gateway";
          opt.GatewayDocsOpenApiInfo = new()
          {
              Title = "My Gateway",
              Version = "v1",
          };
          //opt.DocumentFilter<MyDocumentFilter>();
          opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
          {
              Description = @"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
              Name = "Authorization",
              In = ParameterLocation.Header,
              Type = SecuritySchemeType.ApiKey,
              Scheme = "Bearer"
          });
          opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
          {
              {
                  new OpenApiSecurityScheme
                  {
                      Reference = new OpenApiReference
                      {
                          Type = ReferenceType.SecurityScheme,
                          Id = "Bearer"
                      },
                      Scheme = "oauth2",
                      Name = "Bearer",
                      In = ParameterLocation.Header,
                  },
                  new List<string>()
              }
          });
      });
  });


builder.Services.AddOcelot(builder.Configuration)
    .AddAppConfiguration()
    .AddPolly();


var app = builder.Build();

app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
});


string AlterUpstreamSwaggerJson(HttpContext context, string swaggerJson)
{
    var swagger = JObject.Parse(swaggerJson);
    return swagger.ToString(Formatting.Indented);
}

app.UseSwaggerForOcelotUI(opt =>
{
    opt.ReConfigureUpstreamSwaggerJson = AlterUpstreamSwaggerJson;
    opt.ServerOcelot = "/siteName/apigateway";
    opt.DownstreamSwaggerHeaders = new[]
    {
      new KeyValuePair<string, string>("Auth-Key", "AuthValue"),
  };
}, uiOpt =>
{
    //swaggerUI options
    uiOpt.DocumentTitle = "Gateway documentation";
});

app.MapGet("/", () => "Hello World!");

app.UseOcelot().Wait();
app.Run();
