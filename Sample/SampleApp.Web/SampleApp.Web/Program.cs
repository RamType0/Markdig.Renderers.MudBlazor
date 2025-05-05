using Kroki;
using Kroki.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using MudBlazor.Services;
using SampleApp.Web.Client.Pages;
using SampleApp.Web.Components;
using Yarp.ReverseProxy.Forwarder;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpForwarderWithServiceDiscovery();


builder.Services.AddHttpContextAccessor();
builder.Services.AddKrokiHttpRequestFactory().Configure<IHttpContextAccessor>((options, httpContextAccessor) =>
{
    var request = httpContextAccessor.HttpContext?.Request ?? throw new InvalidOperationException();
    
    options.Endpoint = new($"{request.Scheme}://{request.Host}{request.PathBase}/Kroki/");
});

builder.AddOllamaApiClient("ollama-phi4");

#pragma warning disable SKEXP0070 // 種類は、評価の目的でのみ提供されています。将来の更新で変更または削除されることがあります。続行するには、この診断を非表示にします。
builder.Services.AddKernel().AddOllamaChatCompletion();
#pragma warning restore SKEXP0070 // 種類は、評価の目的でのみ提供されています。将来の更新で変更または削除されることがあります。続行するには、この診断を非表示にします。

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SampleApp.Web.Client._Imports).Assembly);

app.MapForwarder("/Kroki/{**localPath}", "http://kroki", ForwarderRequestConfig.Empty,
    transformBuilderContext => transformBuilderContext.AddPathRemovePrefix("/Kroki"));

app.Run();
