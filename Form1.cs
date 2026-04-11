using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Principal;

namespace frmsaccheck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Cập nhật tiêu đề Form với ngày giờ hệ thống theo múi giờ máy
            this.Text = "Smart App Control" + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Enable KeyPreview để bắt sự kiện phím ESC
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            // Nếu có quyền admin thì tự động chạy lệnh reg query và hiển thị kết quả
            try
            {
                RunRegQueryAndPrint();
            }
            catch (Exception ex)
            {
                txtcmd.Text = "Failed to run reg query: " + ex.Message;
            }

            // Lấy thông tin phiên bản OS thực tế (không phụ thuộc vào manifest)
            int buildNum = GetRealOSVersion().Build;

            if (buildNum >= 22621)
            {
                // Máy hiện đại, Win 11 đời mới -> Hiện thông số SAC
                lblstatus.Text = "Smart App Control is available.";
            }
            else if (buildNum >= 22000)
            {
                // Win 11 đời đầu -> Không có SAC -> Không hỗ trợ, hiện thông báo và thoát
                MessageBox.Show("System Not Supported!\r\nThis application requires Windows 11 Build 22621 or later.", 
                    "Unsupported System", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                this.Close();
            }
            else if (buildNum >= 10240)
            {
                // Windows 10 -> Không có SAC -> Không hỗ trợ, hiện thông báo và thoát
                MessageBox.Show("System Not Supported!\r\nThis application requires Windows 11 Build 22621 or later.", 
                    "Unsupported System", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                // Máy cũ (Win 7/8) -> Không hỗ trợ, hiện thông báo và thoát
                MessageBox.Show("System Not Supported!\r\nThis application requires Windows 11 Build 22621 or later.", 
                    "Unsupported System", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                this.Close();
            }
        }
        //chạy lệnh cmd get value SAC


        // RtlGetVersion cung cấp thông tin phiên bản chính xác từ ntdll (không bị ảnh hưởng bởi compatibility manifest)
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
                // fall back
            }
            return Environment.OSVersion.Version;
        }

        private void RunRegQueryAndPrint()
        {
            string commandArgs = "/c reg query \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\CI\\Policy\" /v VerifiedAndReputablePolicyState";
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
                p.WaitForExit();

                // Parse output để lấy giá trị registry
                string sacValue = ParseRegistryValue(output);
                if (!string.IsNullOrEmpty(sacValue))
                {
                    // Giải mã giá trị SAC và hiển thị
                    string statusText = DecodeSACValue(sacValue);
                    txtcmd.Text = statusText;

                    // Cập nhật trạng thái các button
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
            // Tìm dòng chứa VerifiedAndReputablePolicyState
            string[] lines = output.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (string line in lines)
            {
                // Format: HKEY_LOCAL_MACHINE\...\Policy    VerifiedAndReputablePolicyState    REG_DWORD    0x1
                if (line.Contains("VerifiedAndReputablePolicyState"))
                {
                    // Lấy phần cuối cùng của dòng chứa giá trị hex (0x0, 0x1, 0x2, ...)
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
                // Chuyển từ hex string (0x0, 0x1, 0x2) thành int
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
                        return $"SAC Status: {hexValue} - Unknown state.";
                }
            }
            catch
            {
                return $"SAC Status: {hexValue}";
            }
        }

        private void UpdateButtonStates(string hexValue)
        {
            try
            {
                int value = int.Parse(hexValue.Substring(2), System.Globalization.NumberStyles.HexNumber);

                switch (value)
                {
                    case 0x0: // Tắt (Off)
                        btnactive.Enabled = true;
                        if (btndeactive.Enabled)
                            btndeactive.Enabled = false;
                        break;

                    case 0x1: // Bật (On)
                        btndeactive.Enabled = true;
                        if (btnactive.Enabled)
                            btnactive.Enabled = false;
                        break;

                    case 0x2: // Đánh giá (Evaluation)
                        btndeactive.Enabled = true;
                        if (btnactive.Enabled)
                            btnactive.Enabled = false;
                        break;

                    default:
                        break;
                }
            }
            catch
            {
                // Nếu parse lỗi, vô hiệu hóa cả hai button
                btnactive.Enabled = false;
                btndeactive.Enabled = false;
            }
        }

        private void btnsacmeans_Click(object sender, EventArgs e)
        {
            string fullText = "S.A.C stands for Smart App Control. It is a security feature that helps protect your device\r\nby controlling which apps are allowed to run. More detail";

            lklinfo.Text = fullText;

            // Tìm vị trí của "More detail" trong text để làm link
            string linkText = "More detail";
            int linkStart = fullText.IndexOf(linkText);
            if (linkStart != -1)
            {
                int linkLength = linkText.Length;
                // Tạo LinkArea - chỉ "More detail" là clickable và hiển thị màu xanh
                lklinfo.Links.Clear();
                lklinfo.Links.Add(linkStart, linkLength, "https://learn.microsoft.com/en-us/windows/apps/develop/smart-app-control/overview");
            }
        }

        private void lklinfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Mở URL trong trình duyệt mặc định
                System.Diagnostics.Process.Start((string)e.Link.LinkData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnactive_Click(object sender, EventArgs e)
        {
            // Chạy lệnh reg add để bật SAC (set value to 1)
            RunRegAddCommand(1, "enable");
        }

        private void btndeactive_Click(object sender, EventArgs e)
        {
            // Chạy lệnh reg add để tắt SAC (set value to 0)
            RunRegAddCommand(0, "disable");
        }

        private void RunRegAddCommand(int value, string action)
        {
            try
            {
                string commandArgs = $"/c reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\CI\\Policy\" /v VerifiedAndReputablePolicyState /t REG_DWORD /d {value} /f";
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
                    p.WaitForExit();

                    if (p.ExitCode == 0)
                    {
                        txtcmd.Text = $"Successfully {action}d Smart App Control.";
                        // Refresh lại trạng thái SAC value
                        System.Threading.Thread.Sleep(500); // Đợi registry update
                        RunRegQueryAndPrint();
                    }
                    else
                    {
                        txtcmd.Text = $"Failed to {action} Smart App Control.\r\nError: {err}";
                    }
                }
            }
            catch (Exception ex)
            {
                txtcmd.Text = $"Failed to run command: {ex.Message}";
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                // Làm sáng màu nền button khi di chuột vào
                if (btn.BackColor.ToArgb() == System.Drawing.Color.FromArgb(0, 90, 158).ToArgb())
                {
                    // Button Active: Xanh dương -> sáng hơn
                    btn.BackColor = System.Drawing.Color.FromArgb(0, 120, 190);
                }
                else
                {
                    // Button Deactive và What's S.A.C: Xám -> sáng hơn
                    btn.BackColor = System.Drawing.Color.FromArgb(70, 70, 70);
                }
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                // Phục hồi màu gốc khi rút chuột ra
                if (btn.Name == "btnactive")
                {
                    btn.BackColor = System.Drawing.Color.FromArgb(0, 90, 158);
                }
                else if (btn.Name == "btndeactive" || btn.Name == "btnsacmeans")
                {
                    btn.BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Nhấn ESC để thoát ứng dụng
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }
    }
}
