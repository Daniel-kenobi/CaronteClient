using Barsa.Modules.Interfaces;
using Barsa.Modules.WebService;
using Caronte.Utils.Client;
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
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IWebServiceURLFactory, WebServiceUrls>();
            serviceCollection.AddSingleton<IClientValidation, ClientValidation>();

            serviceCollection.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GeneralConfiguration>());
            serviceCollection.AddHttpClient();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
