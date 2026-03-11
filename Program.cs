using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi("v1");

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        var codespaceName = Environment.GetEnvironmentVariable("CODESPACE_NAME");
        var domain = Environment.GetEnvironmentVariable("CODESPACES_PORT_FORWARDING_DOMAIN")
                     ?? "app.github.dev";

        if (!string.IsNullOrEmpty(codespaceName))
        {
            var port = 5163;
            options.AddServer($"https://{codespaceName}-{port}.{domain}");
        }
    });
// }

var urls = new Dictionary<string, UrlEntry>();

app.MapGet("/", () => "URL SHORTENER");

app.MapPost("/urls", (CreateUrlRequest request) =>
{
    if (string.IsNullOrEmpty(request.url))
    {
        return Results.BadRequest("URL is required");
    }
    var code = Guid.NewGuid().ToString()[..6];
    var entry = new UrlEntry(code, request.url, DateTime.Now);
    urls.Add(code, entry);
    return Results.Created($"urls/{code}", entry);
});

app.MapDelete("/urls/{code}", (string code) =>
{
    return urls.Remove(code) ? Results.NoContent() : Results.NotFound();
});

app.MapGet("/urls/{code}", (string code) =>
{
    return urls.TryGetValue(code, out var entry) ? Results.Redirect(entry.original) : Results.NotFound();
});

app.MapGet("/urls", () => { return Results.Ok(urls.Values.ToList()); });

app.Run();

record UrlEntry(string code, string original, DateTime fecha);

record CreateUrlRequest(string url);