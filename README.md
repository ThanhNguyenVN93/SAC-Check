# Smart App Control (SAC) Manager

A Windows Forms desktop application to check and manage Smart App Control settings on Windows 11 Build 22621 and later.

## Features

✨ **Key Capabilities:**
- 🔍 Automatically check Smart App Control status from Windows Registry
- 🔧 Enable/Disable Smart App Control with administrator privileges
- 🎨 Modern Dark Mode Minimalist UI with Hiku design
- ⌨️ Keyboard shortcuts (ESC to exit)
- 🕐 Real-time system date/time display in title bar
- 📋 Automatic .NET Framework 3.5 detection and activation
- 🖥️ OS version detection and system compatibility check

## System Requirements

- **Windows 11 Build 22621 or later** (Windows 11 with SAC support)
- Administrator privileges required
- .NET Framework 3.5 (auto-enabled at first launch)

## Installation & Usage

### Prerequisites
- Windows 11 Build 22621+
- Administrator account or UAC access

### Running the Application

1. Download the latest release or clone the repository
2. Build the project in Visual Studio 2019+ or .NET Framework 3.5+ compatible IDE
3. Run `frmsaccheck.exe` with administrator privileges
4. The application will:
   - Check .NET 3.5 and enable if needed (frmnetcheck)
   - Display SAC status
   - Allow enabling/disabling SAC

### Keyboard Shortcuts

| Key | Action |
|-----|--------|
| **ESC** | Exit application |

## UI Features

### Dark Mode Theme
- **Background**: Dark Gray (#1E1E1E)
- **Buttons**: 
  - Active (SAC On): Deep Blue (#005A9E)
  - Deactive (SAC Off): Dark Gray (#333333)
  - Info Button: Dark Gray (#333333)
- **Text**: White & Lime Green (Terminal style)
- **Links**: Cyan (#00C8FF)
- **Hover Effects**: Color transitions on button hover

## Building from Source

### Requirements
- Visual Studio 2019 or later
- .NET Framework 3.5
- C# 7.3+

### Build Steps

```bash
# Clone the repository
git clone https://github.com/yourusername/frmsaccheck.git
cd frmsaccheck

# Open the solution
start frmsaccheck.sln

# Build the project
# In Visual Studio: Build > Build Solution
# Or use MSBuild from command line:
msbuild frmsaccheck.sln /p:Configuration=Release
```

## Project Structure

```
frmsaccheck/
├── Form1.cs                 # Main SAC control form
├── Form1.Designer.cs        # UI design for Form1
├── frmnetcheck.cs           # .NET 3.5 checker form
├── frmnetcheck.Designer.cs  # UI design for frmnetcheck
├── Program.cs               # Application entry point
├── app.manifest             # Admin privileges manifest
├── frmsaccheck.csproj       # Project file
└── README.md                # This file
```

## File Descriptions

| File | Purpose |
|------|---------|
| `Form1.cs` | Main application logic for SAC control |
| `Form1.Designer.cs` | UI layout and styling (Dark Mode) |
| `frmnetcheck.cs` | .NET 3.5 detection and auto-enable logic |
| `frmnetcheck.Designer.cs` | .NET checker UI (Dark Mode) |
| `Program.cs` | Entry point with Admin privilege check |
| `app.manifest` | Requests administrator privileges |

## SAC Status Values

| Value | Status | Meaning |
|-------|--------|---------|
| `0x0` | Off | Smart App Control is disabled |
| `0x1` | On | Smart App Control is enabled |
| `0x2` | Evaluation | Smart App Control is in evaluation mode |

## Registry Details

The application modifies the following registry key:

```
HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\CI\Policy
Value: VerifiedAndReputablePolicyState
Type: REG_DWORD
Range: 0x0 (Off), 0x1 (On), 0x2 (Evaluation)
```

## Technical Details

### Key Technologies
- **Language**: C# 7.3
- **Framework**: .NET Framework 3.5
- **UI Framework**: Windows Forms
- **OS Version Detection**: RtlGetVersion from ntdll.dll
- **Registry Access**: Microsoft.Win32.Registry
- **Process Management**: System.Diagnostics.Process

### Privileged Operations
- Registry modification requires administrator privileges
- DISM command for enabling .NET Framework 3.5
- Windows Registry direct access

## Security Considerations

⚠️ **Important:**
- This application requires administrator privileges
- It modifies system registry settings
- Only run from trusted sources
- Changes to SAC affect system security policy

## Troubleshooting

### "System Not Supported" Error
- Your Windows version is not Windows 11 Build 22621 or later
- Update Windows to the latest version

### ".NET 3.5 Failed to Enable"
- Check if .NET Framework 3.5 is available in Windows Features
- Manually enable via `Programs and Features > Turn Windows features on or off`
- Ensure sufficient disk space

### "Failed to Modify Registry"
- Run the application with administrator privileges
- Check if UAC is blocking the operation

### "Unable to Retrieve SAC Value"
- Ensure running as administrator
- Verify Windows version is 11 Build 22621+

## License

This project is provided as-is for educational and personal use.

## Contributing

Contributions are welcome! Please feel free to submit issues and enhancement requests.

## Author

- **frmsaccheck** - Smart App Control Manager for Windows 11

## Disclaimer

This application modifies system settings. Use at your own risk. Always backup your system before making changes to registry settings.

---

**Last Updated**: 2024
**Version**: 1.0
