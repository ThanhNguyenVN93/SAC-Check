using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace frmsaccheck
{
    public partial class frmnetcheck : Form
    {
        public frmnetcheck()
        {
            InitializeComponent();
        }

        private void frmnetcheck_Load(object sender, EventArgs e)
        {
            lblnet35.Text = "Checking .NET 3.5...";
            lblnet35.ForeColor = Color.Gray;

            // Kiểm tra xem .NET 3.5 đã được bật chưa
            if (IsNet35Installed())
            {
                lblnet35.Text = ".NET 3.5 is already installed!";
                lblnet35.ForeColor = Color.Green;
                System.Threading.Thread.Sleep(1500); // Hiển thị 1.5 giây rồi chuyển form
                SwitchToForm1();
            }
            else
            {
                lblnet35.Text = "Enabling .NET 3.5...";
                lblnet35.ForeColor = Color.Orange;

                // Thử bật .NET 3.5
                if (EnableNet35())
                {
                    lblnet35.Text = ".NET 3.5 has been enabled successfully!";
                    lblnet35.ForeColor = Color.Green;
                    System.Threading.Thread.Sleep(1500);
                    SwitchToForm1();
                }
                else
                {
                    lblnet35.Text = "Failed to enable .NET 3.5. Please enable it manually in Windows Features.";
                    lblnet35.ForeColor = Color.Red;
                }
            }
        }

        private bool IsNet35Installed()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5"))
                {
                    if (key != null)
                    {
                        var install = key.GetValue("Install");
                        if (install != null && install.ToString() == "1")
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking .NET 3.5: " + ex.Message);
            }
            return false;
        }

        private bool EnableNet35()
        {
            try
            {
                // Sử dụng DISM để bật .NET Framework 3.5
                string commandArgs = "/online /enable-feature /featurename:NetFx3 /All";
                ProcessStartInfo psi = new ProcessStartInfo("DISM.exe", commandArgs)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    Verb = "runas" // Chạy với quyền admin
                };

                using (Process p = Process.Start(psi))
                {
                    p.WaitForExit();

                    // Kiểm tra lại xem có được bật không
                    System.Threading.Thread.Sleep(2000); // Đợi registry update
                    return IsNet35Installed();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error enabling .NET 3.5: " + ex.Message);
                return false;
            }
        }

        private void SwitchToForm1()
        {
            // Tìm Form1 đang mở hoặc tạo mới
            Form1 form1 = Application.OpenForms["Form1"] as Form1;
            if (form1 == null)
            {
                form1 = new Form1();
            }

            form1.Show();
            form1.Focus();
            form1.BringToFront();

            // Đóng form netcheck hoàn toàn
            this.Close();
        }
    }
}
