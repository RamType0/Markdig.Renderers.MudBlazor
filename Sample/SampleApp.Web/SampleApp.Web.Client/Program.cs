using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Services.AddKrokiHttpRequestFactory().Configure(options => options.Endpoint = new(new(builder.HostEnvironment.BaseAddress), "Kroki/"));
await builder.Build().RunAsync();
