using AiModelOrchestrationSolution.Web;
using AiModelOrchestrationSolution.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOutputCache();

builder.AddKeyedOllamaApiClient(ServiceKeys.DeepSeek).AddKeyedChatClient(ServiceKeys.DeepSeek);
builder.AddKeyedOllamaApiClient(ServiceKeys.Phi).AddKeyedChatClient(ServiceKeys.Phi);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
