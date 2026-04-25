using System;
using System.Drawing;
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
            // Defer so the form paints before any blocking work runs
            this.BeginInvoke(new MethodInvoker(DoNetCheck));
        }

        private void DoNetCheck()
        {
            if (IsNet35Installed())
            {
                lblnet35.Text = ".NET 3.5 is already installed!";
                lblnet35.ForeColor = Color.Green;
                ScheduleTransition(1500);
            }
            else
            {
                lblnet35.Text = "Enabling .NET 3.5...";
                lblnet35.ForeColor = Color.Orange;

                // Run DISM on a background thread so the UI stays responsive
                System.Threading.Thread worker = new System.Threading.Thread(() =>
                {
                    bool success = EnableNet35();
                    this.Invoke(new MethodInvoker(() => OnEnableNet35Complete(success)));
                });
                worker.IsBackground = true;
                worker.Start();
            }
        }

        private void OnEnableNet35Complete(bool success)
        {
            if (success)
            {
                lblnet35.Text = ".NET 3.5 has been enabled successfully!";
                lblnet35.ForeColor = Color.Green;
                ScheduleTransition(1500);
            }
            else
            {
                lblnet35.Text = "Failed to enable .NET 3.5. Please enable it manually in Windows Features.";
                lblnet35.ForeColor = Color.Red;
            }
        }

        private void ScheduleTransition(int delayMs)
        {
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer { Interval = delayMs };
            t.Tick += (s, args) => { t.Stop(); t.Dispose(); SwitchToForm1(); };
            t.Start();
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
                // Bug fix: Verb = "runas" is incompatible with UseShellExecute = false and was silently ignored.
                // The application already runs as administrator, so elevation is not needed here.
                string commandArgs = "/online /enable-feature /featurename:NetFx3 /All";
                ProcessStartInfo psi = new ProcessStartInfo("DISM.exe", commandArgs)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process p = Process.Start(psi))
                {
                    if (!p.WaitForExit(300000)) // 5-minute timeout for DISM
                    {
                        p.Kill();
                        return false;
                    }
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
            Form1 form1 = Application.OpenForms["Form1"] as Form1;
            if (form1 == null)
            {
                form1 = new Form1();
            }

            form1.Show();
            form1.Focus();
            form1.BringToFront();

            this.Close();
        }
    }
}
