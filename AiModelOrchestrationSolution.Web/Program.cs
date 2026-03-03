using AiModelOrchestrationSolution.Web;
using AiModelOrchestrationSolution.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddKeyedOllamaApiClient(ServiceKeys.DeepSeek).AddKeyedChatClient(ServiceKeys.DeepSeek);
builder.AddKeyedOllamaApiClient(ServiceKeys.Phi).AddKeyedChatClient(ServiceKeys.Phi);
builder.AddKeyedOllamaApiClient(ServiceKeys.Llama).AddKeyedChatClient(ServiceKeys.Llama);

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.MapDefaultEndpoints();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseStaticFiles();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();