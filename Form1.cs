using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace frmsaccheck
{
    public partial class Form1 : Form
    {
        private const string RegistryKeyPath = "HKLM\\SYSTEM\\CurrentControlSet\\Control\\CI\\Policy";
        private const string RegistryValueName = "VerifiedAndReputablePolicyState";
        private const int ProcessTimeoutMs = 30000;

        public Form1()
        {
            InitializeComponent();
        }

        // Bug fix: Application.Run() with no form never exits on its own.
        // Calling Application.Exit() here ensures the process terminates when Form1 closes.
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = "Smart App Control" + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            int buildNum = GetRealOSVersion().Build;

            if (buildNum >= 22621)
            {
                lblstatus.Text = "Smart App Control is available.";
                try
                {
                    RunRegQueryAndPrint();
                }
                catch (Exception ex)
                {
                    txtcmd.Text = "Failed to run reg query: " + ex.Message;
                }
            }
            else
            {
                MessageBox.Show("System Not Supported!\r\nThis application requires Windows 11 Build 22621 or later.",
                    "Unsupported System",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct RTL_OSVERSIONINFOEX
        {
            public uint dwOSVersionInfoSize;
            public uint dwMajorVersion;
            public uint dwMinorVersion;
            public uint dwBuildNumber;
            public uint dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
        }

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int RtlGetVersion(ref RTL_OSVERSIONINFOEX lpVersionInformation);

        private Version GetRealOSVersion()
        {
            try
            {
                RTL_OSVERSIONINFOEX osvi = new RTL_OSVERSIONINFOEX();
                osvi.dwOSVersionInfoSize = (uint)Marshal.SizeOf(typeof(RTL_OSVERSIONINFOEX));
                int result = RtlGetVersion(ref osvi);
                if (result == 0)
                {
                    return new Version((int)osvi.dwMajorVersion, (int)osvi.dwMinorVersion, (int)osvi.dwBuildNumber);
                }
            }
            catch
            {
                // fall back to Environment.OSVersion
            }
            return Environment.OSVersion.Version;
        }

        private void RunRegQueryAndPrint()
        {
            string commandArgs = "/c reg query \"" + RegistryKeyPath + "\" /v " + RegistryValueName;
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", commandArgs)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (Process p = Process.Start(psi))
            {
                string output = p.StandardOutput.ReadToEnd();
                string err = p.StandardError.ReadToEnd();

                if (!p.WaitForExit(ProcessTimeoutMs))
                {
                    p.Kill();
                    txtcmd.Text = "Command timed out while querying registry.";
                    return;
                }

                string sacValue = ParseRegistryValue(output);
                if (!string.IsNullOrEmpty(sacValue))
                {
                    txtcmd.Text = DecodeSACValue(sacValue);
                    UpdateButtonStates(sacValue);
                }
                else if (!string.IsNullOrEmpty(err))
                {
                    txtcmd.Text = "ERROR: " + err;
                }
                else
                {
                    txtcmd.Text = "Unable to retrieve SAC value. Please ensure you have administrator privileges.";
                }
            }
        }

        private string ParseRegistryValue(string output)
        {
            string[] lines = output.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (string line in lines)
            {
                if (line.Contains(RegistryValueName))
                {
                    string[] parts = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0)
                    {
                        string lastPart = parts[parts.Length - 1];
                        if (lastPart.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                        {
                            return lastPart;
                        }
                    }
                }
            }
            return null;
        }

        private string DecodeSACValue(string hexValue)
        {
            try
            {
                int value = int.Parse(hexValue.Substring(2), System.Globalization.NumberStyles.HexNumber);

                switch (value)
                {
                    case 0x0:
                        return "SAC Status: 0x0 (Off) - Smart App Control is disabled.";
                    case 0x1:
                        return "SAC Status: 0x1 (On) - Smart App Control is enabled.";
                    case 0x2:
                        return "SAC Status: 0x2 (Evaluation) - Smart App Control is in evaluation mode.";
                    default:
                        return "SAC Status: " + hexValue + " - Unknown state.";
                }
            }
            catch
            {
                return "SAC Status: " + hexValue;
            }
        }

        private void UpdateButtonStates(string hexValue)
        {
            try
            {
                int value = int.Parse(hexValue.Substring(2), System.Globalization.NumberStyles.HexNumber);

                switch (value)
                {
                    case 0x0: // Off
                        btnactive.Enabled = true;
                        btndeactive.Enabled = false;
                        break;

                    case 0x1: // On
                    case 0x2: // Evaluation
                        btndeactive.Enabled = true;
                        btnactive.Enabled = false;
                        break;

                    default:
                        break;
                }
            }
            catch
            {
                btnactive.Enabled = false;
                btndeactive.Enabled = false;
            }
        }

        private void btnsacmeans_Click(object sender, EventArgs e)
        {
            string fullText = "S.A.C stands for Smart App Control. It is a security feature that helps protect your device\r\nby controlling which apps are allowed to run. More detail";

            lklinfo.Text = fullText;

            string linkText = "More detail";
            int linkStart = fullText.IndexOf(linkText);
            if (linkStart != -1)
            {
                lklinfo.Links.Clear();
                lklinfo.Links.Add(linkStart, linkText.Length, "https://learn.microsoft.com/en-us/windows/apps/develop/smart-app-control/overview");
            }
        }

        private void lklinfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start((string)e.Link.LinkData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnactive_Click(object sender, EventArgs e)
        {
            RunRegAddCommand(1, "enable");
        }

        private void btndeactive_Click(object sender, EventArgs e)
        {
            RunRegAddCommand(0, "disable");
        }

        private void RunRegAddCommand(int value, string action)
        {
            try
            {
                string commandArgs = "/c reg add \"" + RegistryKeyPath + "\" /v " + RegistryValueName + " /t REG_DWORD /d " + value + " /f";
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", commandArgs)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process p = Process.Start(psi))
                {
                    string output = p.StandardOutput.ReadToEnd();
                    string err = p.StandardError.ReadToEnd();

                    if (!p.WaitForExit(ProcessTimeoutMs))
                    {
                        p.Kill();
                        txtcmd.Text = "Command timed out while writing registry.";
                        return;
                    }

                    if (p.ExitCode == 0)
                    {
                        txtcmd.Text = "Successfully " + action + "d Smart App Control.";
                        // Registry is written synchronously; re-query immediately
                        RunRegQueryAndPrint();
                    }
                    else
                    {
                        txtcmd.Text = "Failed to " + action + " Smart App Control.\r\nError: " + err;
                    }
                }
            }
            catch (Exception ex)
            {
                txtcmd.Text = "Failed to run command: " + ex.Message;
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                if (btn.BackColor.ToArgb() == Color.FromArgb(0, 90, 158).ToArgb())
                {
                    btn.BackColor = Color.FromArgb(0, 120, 190);
                }
                else
                {
                    btn.BackColor = Color.FromArgb(70, 70, 70);
                }
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                if (btn.Name == "btnactive")
                {
                    btn.BackColor = Color.FromArgb(0, 90, 158);
                }
                else if (btn.Name == "btndeactive" || btn.Name == "btnsacmeans")
                {
                    btn.BackColor = Color.FromArgb(51, 51, 51);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }
    }
}
