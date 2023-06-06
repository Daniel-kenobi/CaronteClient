using Barsa.Models.Errors;
using Barsa.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Barsa.Modules.Interfaces;
using Barsa.Modules.Errors.HandleExceptions;

namespace Caronte.Modules.Information.GetClientPrintScreen
{
    public class PrintScreenQueryHandler : HandleExceptions, IRequestHandler<PrintScreenQuery, CommonResponse>
    {
        public PrintScreenQueryHandler(IHttpClientFactory httpClient, IWebServiceURLFactory urlFactory) : base(httpClient, urlFactory)
        {

        }

        public async Task<CommonResponse> Handle(PrintScreenQuery request, CancellationToken cancellationToken)
        {
            var response = new CommonResponse();

            try
            {
                int screenLeft = SystemInformation.VirtualScreen.Left;
                int screenTop = SystemInformation.VirtualScreen.Top;
                int screenWidth = SystemInformation.VirtualScreen.Width;
                int screenHeight = SystemInformation.VirtualScreen.Height;

                using (Bitmap bitmap = new(screenWidth, screenHeight))
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.CopyFromScreen(screenLeft, screenTop, 0, 0, bitmap.Size);
                    }

                    bitmap.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{DateTime.Now.ToString("ddMMyyyyssff")}Shot.png"));
                }
            }
            catch (Exception ex)
            {
                await Handle(ex);
                response.AddErrors(new Errors(ErrorType.BadRequest, ex?.Message, new List<Exception>() { ex }));
            }

            return response;
        }
    }
}