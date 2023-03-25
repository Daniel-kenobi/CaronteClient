using Caronte.Configurations;
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
            var configureApplication = new ConfigureApplication();
            var mediator = configureApplication.ConfigureApplicationServices().GetRequiredService<IMediator>();

            configureApplication.ConfigureApplicationStyles();

            await InitializeExecution(mediator, default);
        }

        private static async Task InitializeExecution(IMediator mediator, System.Threading.CancellationToken cancellationToken)
        {
            var initializeModules = new InitializeModules(mediator, cancellationToken);
            await initializeModules.Initialize();
        }
    }
}
