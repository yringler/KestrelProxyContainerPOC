using Proxy;
using Yarp.ReverseProxy.Forwarder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSystemWebAdapters();
builder.Services.AddHttpForwarder();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

var transformer = new BranchTransformer();
var requestConfig = new ForwarderRequestConfig { ActivityTimeout = TimeSpan.FromSeconds(100) };

app.UseRouting();
app.UseAuthorization();
app.UseSystemWebAdapters();

app.MapDefaultControllerRoute();

// This is where the YARP magic happens right now. Very basic, because this forwards everything right now 
app.MapForwarder("/{**catch-all}", "", requestConfig, transformer)
    .Add(static builder => ((RouteEndpointBuilder)builder).Order = int.MaxValue);

app.Run();