var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder.AddOllama("ollama")
    .WithGPUSupport()
    .WithDataVolume()
    .WithOpenWebUI();
    
var deepSeekModel = ollama.AddModel("deepseek", "deepseek-r1:1.5b");
var phiModel = ollama.AddModel("phi", "phi3:3.8b");
var llamaModel = ollama.AddModel("llama", "llama3.2-vision:11b");

builder.AddProject<Projects.AiModelOrchestrationSolution_Web>("web")
    .WithExternalHttpEndpoints()
    .WithReference(deepSeekModel)
    .WithReference(phiModel)
    .WithReference(llamaModel)
    .WaitFor(deepSeekModel)
    .WaitFor(phiModel)
    .WaitFor(llamaModel);

builder.Build().Run();
