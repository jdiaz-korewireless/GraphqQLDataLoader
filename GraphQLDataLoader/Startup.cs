using GraphiQl;
using GraphQLDataLoader.Data;
using GraphQLDataLoader.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphQLDataLoader
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<SchoolContext>(opts => opts.UseSqlServer(connectionString));
            //services.AddDbContext<SchoolContext>(opts => opts.UseSqlServer(connectionString), ServiceLifetime.Singleton);
            //services.AddDbContext<SchoolContext>(opts => opts.UseSqlServer(connectionString), ServiceLifetime.Scoped);
            services.AddDbContext<SchoolContext>(opts => opts.UseSqlServer(connectionString), ServiceLifetime.Singleton);

            services.AddSingleton<ICourseStore, CourseStore>();
            services.AddSingleton<IInstructorStore, InstructorStore>();
            services.AddGraphQlServices();
            services.AddGraphTypes();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //TrySeedDatabase();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //    endpoints.MapPost("/graphql", async context =>
            //    {

            //    });
            //});

            app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings
            {
                BuildUserContext = ctx => new GraphQLUserContext
                {
                    User = ctx.User
                },
                EnableMetrics = Configuration.GetValue<bool>("EnableMetrics"),
                ExposeExceptions = Configuration.GetValue<bool>("ExposeExceptions")
            });
            //app.UseMiddleware<GraphQLMiddleware>();
            app.UseGraphiQl("/graphiql", "/graphql");
            app.UseDefaultFiles();
            app.UseStaticFiles();

        }

        private void TrySeedDatabase()
        {
            using (var db = new SchoolContext())
            {
                DbInitializer.Initialize(db);
            }
        }
    }
}
