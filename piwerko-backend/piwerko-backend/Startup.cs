using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Piwerko.Api.Repo;
using Microsoft.EntityFrameworkCore;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Services;
using Piwerko.Api.Helpers;

namespace Piwerko.Api
{
    public class Startup
    {
        private IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));
            services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));
            services.AddSingleton<IConfiguration>(Configuration);


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBeerRepository, BeerRepository>();
            services.AddScoped<IBeerService, BeerService>();
            services.AddScoped<IBreweryService, BreweryService>();
            services.AddScoped<IBreweryRepository, BreweryRepository>();
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseMvc();
        }
    }
}
