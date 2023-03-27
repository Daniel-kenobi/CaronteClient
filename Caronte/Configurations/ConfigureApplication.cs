using Caronte.Modules.Logger;
using CaronteLib.Implementations;
using CaronteLib.Interfaces;
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

            services.AddSingleton<IWebServiceData, WebServiceData>();
            services.AddMediatR(typeof(GetKeyboardLogQuery));
            services.AddHttpClient();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
