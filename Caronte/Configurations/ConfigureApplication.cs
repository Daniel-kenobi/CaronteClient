using Caronte.Modules.Logger;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;

namespace Caronte.Configurations
{
    public class ConfigureApplication
    {
        public void ConfigureApplicationStyles()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        public ServiceProvider ConfigureApplicationServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddMediatR(typeof(LogQuery));
            services.AddHttpClient();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
