using Microsoft.AspNetCore.Http;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Make sure a JS engine is registered, or you will get an error!
builder.Services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
  .AddV8();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddReact();
builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}

// Initialise ReactJS.NET. Must be before static files.
app.UseReact(config =>
{
	// If you want to use server-side rendering of React components,
	// add all the necessary JavaScript files here. This includes
	// your components as well as all of their dependencies.
	// See http://reactjs.net/ for more information. Example:
	config
	  .AddScript("~/js/remarkable.min.js")
	  .AddScript("~/js/tutorial.jsx")
	  .SetJsonSerializerSettings(new JsonSerializerSettings
	  {
		  StringEscapeHandling = StringEscapeHandling.EscapeHtml,
		  ContractResolver = new CamelCasePropertyNamesContractResolver()
	  });

	// If you use an external build tool (for example, Babel, Webpack,
	// Browserify or Gulp), you can improve performance by disabling
	// ReactJS.NET's version of Babel and loading the pre-transpiled
	// scripts. Example:
	//config
	//  .SetLoadBabel(false)
	//  .AddScriptWithoutTransform("~/Scripts/bundle.server.js");
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
