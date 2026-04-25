using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;

namespace frmsaccheck
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Kiểm tra xem ứng dụng có chạy dưới quyền Administrator không
            if (!IsRunAsAdmin())
            {
                // Nếu không phải admin, restart với quyền admin
                RestartAsAdmin();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Khởi động frmnetcheck trước để kiểm tra .NET 3.5
            // Sử dụng ApplicationContext để quản lý vòng đời của ứng dụng
            frmnetcheck netcheck = new frmnetcheck();
            netcheck.Show();

            Application.Run();
        }

        /// <summary>
        /// Kiểm tra xem ứng dụng đang chạy dưới quyền Administrator
        /// </summary>
        private static bool IsRunAsAdmin()
        {
            try
            {
                WindowsIdentity id = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(id);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Restart ứng dụng với quyền Administrator
        /// </summary>
        private static void RestartAsAdmin()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo()
                {
                    FileName = Application.ExecutablePath,
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(psi);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to restart as administrator.\r\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Environment.Exit(1);
            }
        }
    }
}
