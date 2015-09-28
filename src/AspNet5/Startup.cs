namespace AspNet5
{
    using Services;
    using Autofac;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;
    using Microsoft.Framework.DependencyInjection;
    using System;
    using Autofac.Framework.DependencyInjection;
    using Repositories;
    using Models;
    using Serilog;
    using SerilogWeb.Classic.Enrichers;
    using SerilogWeb.Classic;
    using Logging;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var builder = new ContainerBuilder();

            builder.RegisterType<ConcreteCacheProvider>().As<ICacheProvider>();
            builder.RegisterType<SerilogLoggerFactory>().As<ILoggerFactory>();

            builder.RegisterType(typeof(ThingyRepository)).Named<IRepository<Thingy>>("repository");

            builder.RegisterGenericDecorator(
                    typeof(CachedRepository<>),
                    typeof(IRepository<>),
                    fromKey: "repository")
                    .Keyed("decorated", typeof(IRepository<>));

            builder.RegisterGenericDecorator(
                    typeof(PerformanceLoggingRepository<>),
                    typeof(IRepository<>),
                    fromKey: "decorated");

            builder.Populate(services);

            var container = builder.Build();

            return container.ResolveOptional<IServiceProvider>();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {            
            app.UseStaticFiles();
            app.UseMvc();
            
            Log.Logger = new LoggerConfiguration()
                                .WriteTo.File(@"C:\work\serilog.txt", outputTemplate: "BLAH {Timestamp} [{Level}] ({HttpRequestId}|{UserName}) ({SourceContext}) {Message}{NewLine}{Exception}")
                                .Enrich.With<HttpRequestIdEnricher>()
                                .MinimumLevel.Verbose()
                                .CreateLogger();
        }
    }
}
