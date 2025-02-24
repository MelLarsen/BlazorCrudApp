using BlazorCrudApp.Clients.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register HttpClient
builder.Services.AddHttpClient("MyClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7243/");
});

// Ensure WebSocket Support for Blazor Server
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Middleware Order Fix: Ensure Authentication & Authorization come first
app.UseRouting();

// Anti-Forgery Middleware Must Be AFTER Routing & Authentication
app.UseAntiforgery();


// Ensure Components Render in Interactive Mode
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
