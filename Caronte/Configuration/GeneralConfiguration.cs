using Barsa.Implementations;
using Barsa.Interfaces;
using Caronte.Modules.Information.GetKeyboardLog;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;

namespace Caronte.Configuration
{
    public class GeneralConfiguration
    {
        public void ConfigureAttributes()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        public ServiceProvider ConfigureServices()
        {
            IServiceCollection serviceColection = new ServiceCollection();

            serviceColection.AddSingleton<IWebServiceURLFactory, WebServiceData>();
            serviceColection.AddMediatR(typeof(GetKeyboardLogQuery));
            serviceColection.AddHttpClient();

            var serviceProvider = serviceColection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
