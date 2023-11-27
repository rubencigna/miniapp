using System.Text.Json.Serialization;
using Services;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

var sampleTodos = new Todo[] {
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
};

var newlist = new List<Todo>();
for (int i = 0; i < 1000; i++)
{
    var newitem = new Todo(i, "title " + i, DateOnly.FromDateTime(DateTime.Now), true);
    newlist.Add(newitem);
};

var allsampleTodos = sampleTodos.Concat(newlist).ToArray();

// todos
var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => allsampleTodos);
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound()).CacheOutput();

// ping
var pingEndpoint = app.MapGroup("/ping");
pingEndpoint.MapPost("/", async (HttpRequest request) =>
{
    var result = await request.ReadFromJsonAsync<Todo>();
    return Results.Ok(result);
});

// auth
var authEndpoint = app.MapGroup("/Api/Auth");
authEndpoint.MapPost("/Validate.do", async (HttpRequest request) =>
{
    bool result = false;
    var auth = await request.ReadFromJsonAsync<AuthModel>();
    if (auth?.username == "admin" && auth.password == "admin")
    {
        result = true;
    }
    return Results.Ok(result);
});

// customer
var customerEndpoint = app.MapGroup("/Api/Customer");
customerEndpoint.MapPost("/Create.do", async (HttpRequest request) =>
{
    var payload = await request.ReadFromJsonAsync<CustomerModel>();
    var isOK = CustomerService.Create(payload!);
    if (isOK)
        return Results.Ok(payload);
    return Results.NoContent();
});
customerEndpoint.MapPost("/GetAll.do", async (HttpRequest request) =>
{
    var payload = await request.ReadFromJsonAsync<CustomerModel>();
    var customers = CustomerService.Read(payload!);
    if (customers.Count == 0)
        return Results.NoContent();

    var result = new List<CustomerModel>();
    foreach (CustomerModel item in customers)
    {
        result.Add(item);
    }
    return Results.Ok(result);
});

// user



app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

public record TransactionModel(int Id, string Message);
public record AuthModel(string username, string password);
public record CustomerModel(string Name, string Email, string Phone);

[JsonSerializable(typeof(Todo[]))]
[JsonSerializable(typeof(TransactionModel[]))]
[JsonSerializable(typeof(AuthModel[]))]
[JsonSerializable(typeof(CustomerModel[]))]
[JsonSerializable(typeof(List<CustomerModel>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}