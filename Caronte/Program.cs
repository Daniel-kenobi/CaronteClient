using Caronte.Modules.Logger;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Caronte
{
    internal static class Program
    {
        [STAThread]
        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddMediatR(typeof(LogQuery));

            var serviceProvider = services.BuildServiceProvider();
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            await InitializeModules.Initialize(mediator, default);
        }
    }
}
