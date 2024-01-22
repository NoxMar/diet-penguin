using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DietPenguin.Gui;

using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using DietPenguin.Gui.Api;
using DietPenguin.Gui.User;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
    .AddBlazorise( options =>
    {
        options.Immediate = true;
    } )
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IBackendApiClient, BackendApiClient>(httpClient =>
{
     httpClient.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]
                                   ?? builder.HostEnvironment.BaseAddress);
});
builder.Services.AddScoped<AppState>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

await builder.Build().RunAsync();