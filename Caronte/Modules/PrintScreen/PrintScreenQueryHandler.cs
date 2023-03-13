using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caronte.Modules.PrintScreen
{
    public class PrintScreenQueryHandler : IRequestHandler<PrintScreenQuery>
    { 
        public Task<Unit> Handle(PrintScreenQuery request, CancellationToken cancellationToken)
        {
            System.Timers.Timer timer = new(request.SecondsToGetScreenshots * 1000);
            timer.Elapsed += TakeScreenShot;
            timer.Start();

            return Task.FromResult(Unit.Value);
        }

        private void TakeScreenShot(object source, System.Timers.ElapsedEventArgs e)
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
    }
}