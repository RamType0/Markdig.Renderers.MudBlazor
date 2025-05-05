var builder = DistributedApplication.CreateBuilder(args);

var kroki = builder.AddKrokiServer("kroki")
    .WithKrokiMermaidServer()
    .WithKrokiBpmnServer()
    .WithKrokiExcalidrawServer()
    .WithKrokiDiagramsNetServer();

var ollama = builder.AddOllama("ollama").WithDataVolume().WithOpenWebUI().WithGPUSupport();

var phi4 = ollama.AddModel("phi4");

builder.AddProject<Projects.SampleApp_Web>("web")
    .WithExternalHttpEndpoints()
    .WithReference(kroki)
    .WaitFor(kroki)
    .WithReference(phi4)
    .WaitFor(phi4);

builder.Build().Run();
