using CaronteLib.Response;
using MediatR;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caronte.Modules.Information.GetClientPrintScreen
{
    public class PrintScreenQueryHandler : IRequestHandler<CommomMediatorResponses, CommomMediatorResponses>
    {
        public Task<CommomMediatorResponses> Handle(PrintScreenQuery request, CancellationToken cancellationToken)
        {
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

                return Task.FromResult(Unit.Value);
            }
            catch (Exception ex)
            {

            }
        }
    }
}