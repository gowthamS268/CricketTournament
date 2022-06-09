using Cricket.Domain;
using Cricket.Generator;
using Cricket.Services;

namespace Cricket
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var shotOutcomes = new ShotOutcomeGenerator().Generate();
            var teams = new TeamGenerator().Generate();
            var matches = new List<IMatch>();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddTransient<IMatchService, MatchService>();
            services.AddTransient<IInningsService, InningsService>();
            services.AddTransient<IBallOutcomeService, BallOutcomeService>();
            services.AddSingleton((IServiceProvider arg) => shotOutcomes);
            services.AddSingleton((IServiceProvider arg) => matches);
            services.AddSingleton((IServiceProvider arg) => teams);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
