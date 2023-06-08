using Caronte.Configuration;
using Caronte.Modules;
using Caronte.Utils.Client;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte
{
    internal static class Program
    {
        [STAThread]
        static async Task Main()
        {
            GeneralConfiguration configureApplication = new();
            
            var mediator = configureApplication.ConfigureServices().GetRequiredService<IMediator>();
            var clientValidation = configureApplication.ConfigureServices().GetRequiredService<IClientValidation>();

            configureApplication.ConfigureAttributes();

            await InitializeExecution(mediator, clientValidation);
        }

        private static async Task InitializeExecution(IMediator mediator, IClientValidation clientValidation)
        {
            var initializeModules = new ModuleExecution(mediator, clientValidation);
            await initializeModules.Execute();
        }
    }
}
