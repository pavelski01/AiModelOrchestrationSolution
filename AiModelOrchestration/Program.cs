var builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<OpenWebUIResource>? openWebUi = default;

var ollama = builder.AddOllama("ollama")
    .WithGPUSupport()
    .WithDataVolume()
    .WithOpenWebUI(owu => openWebUi = owu);
    
var deepSeekModel = ollama.AddModel("deepseek", "deepseek-r1:1.5b");
var phiModel = ollama.AddModel("phi", "phi3:3.8b");
var llamaModel = ollama.AddModel("llama", "llama3.2-vision:11b");

builder.AddProject<Projects.AiModelOrchestrationSolution_Web>("web")
    .WithExternalHttpEndpoints()
    .WithReference(deepSeekModel)
    .WithReference(phiModel)
    .WithReference(llamaModel)
    .WaitFor(ollama)
    .WaitFor(openWebUi ?? throw new InvalidOperationException("Cannot run Open Web UI"))
    .WaitFor(deepSeekModel)
    .WaitFor(phiModel)
    .WaitFor(llamaModel);

builder.Build().Run();
