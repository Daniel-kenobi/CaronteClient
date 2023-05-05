using Barsa.Abstracts;
using Barsa.Interfaces;
using Barsa.Models.Enums;
using Barsa.Models.Errors;
using Barsa.Commoms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caronte.Modules.Information.GetClientPrintScreen
{
    public class PrintScreenQueryHandler : HandleClientExceptions, IRequestHandler<PrintScreenQuery, CommomResponse>
    {
        public PrintScreenQueryHandler(IHttpClientFactory httpClient, IWebServiceURLFactory urlFactory) : base(httpClient, urlFactory)
        {

        }

        public async Task<CommomResponse> Handle(PrintScreenQuery request, CancellationToken cancellationToken)
        {
            var response = new CommomResponse();

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
                response.AddErrors(new MediatorErrors(ErrorType.BadRequest, ex?.Message, new List<Exception>() { ex }));
            }

            return response;
        }
    }
}