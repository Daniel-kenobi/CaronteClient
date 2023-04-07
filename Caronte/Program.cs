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
            ConfigureApplication configureApplication = new();
            
            var mediator = configureApplication.ConfigureApplicationServices().GetRequiredService<IMediator>();
            configureApplication.ConfigureApplicationAttributes();

            await InitializeExecution(mediator, default);
        }

        private static async Task InitializeExecution(IMediator mediator, System.Threading.CancellationToken cancellationToken)
        {
            var initializeModules = new InitializeModules(mediator, cancellationToken);
            await initializeModules.Initialize();
        }
    }
}
