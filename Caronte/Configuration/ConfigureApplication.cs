using Caronte.Modules.Logger;
using CaronteLib.Implementations;
using CaronteLib.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;

namespace Caronte.Configuration
{
    public class ConfigureApplication
    {
        public void ConfigureApplicationAttributes()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        public ServiceProvider ConfigureApplicationServices()
        {
            IServiceCollection serviceColection = new ServiceCollection();

            serviceColection.AddSingleton<IWebServiceData, WebServiceData>();
            serviceColection.AddMediatR(typeof(GetKeyboardLogQuery));
            serviceColection.AddHttpClient();

            var serviceProvider = serviceColection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
