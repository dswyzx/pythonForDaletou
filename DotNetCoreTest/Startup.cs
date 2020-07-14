using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;

namespace AutoGetDLT
{
    public class Startup
    {

        Logger logger = LogManager.GetCurrentClassLogger();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddMvc();
            // services.AddDbContext<EFDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            //  services.AddTimedJob();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // env.ConfigureNLog(Path.Combine(env.WebRootPath, "nlog.config"));

            // app.UseTimedJob();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                LogHelper.Info(context.Request.Path);
                logger.Info(context.Request.Path);
                await next.Invoke();
                //using (var bodyReader = new StreamReader(context.Request.Body))
                //{
                //    string body = await bodyReader.ReadToEndAsync();
                //    //LogHelper.Info(body);
                //    //context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
                //    await next.Invoke();
                //    // context.Request.Body = initialBody;
                //}
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            ConstsConf.WWWRootPath = env.WebRootPath;
            ConstsConf.txtName = Configuration["TextName:DLTJSON"];
            //string y = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build()["ConnectionStrings:SqlServer"];
        }
    }
}
