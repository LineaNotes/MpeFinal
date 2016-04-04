using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MpeFinal.Models;
using Microsoft.AspNet.Localization;

namespace MpeFinal
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

      if (env.IsDevelopment())
      {
        builder.AddUserSecrets();
      }

      builder.AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddEntityFramework()
        .AddSqlServer()
        .AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

      services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

      services.AddMvc()
        .AddViewLocalization()
        .AddDataAnnotationsLocalization();

      services.AddAuthorization(options =>
      {
        options.AddPolicy("HighLevelRole", policy => policy.RequireRole("administrator", "upravnik"));
      });

      services.AddTransient<GasContextSeedData>();
    }

    public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
      GasContextSeedData seeder)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      var requestLocalizationOptions = new RequestLocalizationOptions
      {
        SupportedCultures = new List<CultureInfo>
        {
          new CultureInfo("en-IE")
        },
        SupportedUICultures = new List<CultureInfo>
        {
          new CultureInfo("en-IE")
        },
        RequestCultureProviders = new List<IRequestCultureProvider>
        {
          new CustomRequestCultureProvider(httpContext => Task.FromResult(new ProviderCultureResult("en-IE"))),
          new AcceptLanguageHeaderRequestCultureProvider()
        }
      };

      app.UseRequestLocalization(requestLocalizationOptions, new RequestCulture("en-IE"));

      if (env.IsDevelopment())
      {
        app.UseBrowserLink();
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");

        try
        {
          using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
          {
            serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
              .Database.Migrate();
          }
        }
        catch
        {
        }
      }

      app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());
      app.UseStaticFiles();
      app.UseIdentity();
      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "default",
          template: "{controller=Home}/{action=Index}/{id?}");
      });

      await seeder.EnsureSeedDataAsync();
    }

    public static void Main(string[] args) => WebApplication.Run<Startup>(args);
  }
}