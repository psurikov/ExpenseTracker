using ExpenseTracker.Database;

namespace ExpenseTracker.Server
{
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of Startup class.
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The method is called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseStorage();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddControllers();
        }

        /// <summary>
        /// Configures the specified application.
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="applicationBuilder">The application</param>
        /// <param name="webHostEnvironment">The environment</param>
        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
                applicationBuilder.UseSwagger();
                applicationBuilder.UseSwaggerUI();
            }
            
            applicationBuilder.UseDefaultFiles();
            applicationBuilder.UseStaticFiles();
            applicationBuilder.UseRouting();
            applicationBuilder.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}