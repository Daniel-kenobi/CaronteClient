using Caronte.Modules.Information.GetKeyboardLog;
using Caronte.Utils.ApiUrl;
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

            serviceColection.AddSingleton<IWebServiceURL, WebServiceUrls>();
            serviceColection.AddMediatR(typeof(GetKeyboardLogQuery));
            serviceColection.AddHttpClient();

            var serviceProvider = serviceColection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
