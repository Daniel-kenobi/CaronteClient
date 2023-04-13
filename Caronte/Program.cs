using Caronte.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Caronte
{
    internal static class Program
    {
        [STAThread]
        static async Task Main(string[] args)
        {
            Configuration.ApplicationConfiguration configureApplication = new();
            
            var mediator = configureApplication.ConfigureServices().GetRequiredService<IMediator>();
            configureApplication.ConfigureAttributes();

            await InitializeExecution(mediator, default);
        }

        private static async Task InitializeExecution(IMediator mediator, System.Threading.CancellationToken cancellationToken)
        {
            var initializeModules = new InitializeModules(mediator, cancellationToken);
            await initializeModules.Initialize();
        }
    }
}
