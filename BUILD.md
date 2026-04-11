# Build Instructions

## Prerequisites

- **Operating System**: Windows 11 Build 22621 or later
- **IDE**: Visual Studio 2019, 2022, or 2026+
- **.NET Framework**: 3.5+ SDK
- **Administrator Access**: Required for building and testing

## Building the Project

### Method 1: Using Visual Studio GUI

1. Open `frmsaccheck.sln` in Visual Studio
2. Select `Build > Build Solution` (or press `Ctrl+Shift+B`)
3. Wait for the build to complete
4. Output files will be in `bin/Release/` or `bin/Debug/`

### Method 2: Using MSBuild Command Line

```bash
# Navigate to project directory
cd D:\code\frmsaccheck\

# Build in Release mode
msbuild frmsaccheck.sln /p:Configuration=Release

# Build in Debug mode
msbuild frmsaccheck.sln /p:Configuration=Debug
```

### Method 3: Using dotnet CLI

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build --configuration Release
```

## Output Locations

- **Debug Build**: `bin/Debug/frmsaccheck.exe`
- **Release Build**: `bin/Release/frmsaccheck.exe`

## Running the Application

### From Visual Studio
1. Press `F5` to run with debugging
2. Or press `Ctrl+F5` to run without debugging

### From Command Line

```bash
# Run debug build
bin\Debug\frmsaccheck.exe

# Run release build
bin\Release\frmsaccheck.exe
```

### Important: Administrator Privileges

The application requires administrator privileges. When running:

- **From VS**: Visual Studio must run as administrator
- **From CLI**: Open PowerShell as Administrator
- **Direct**: Right-click `.exe` → Run as Administrator

## Build Configuration

### Debug Configuration
- Optimizations: Disabled
- Debug symbols: Enabled
- File size: Larger (~5-10 MB)
- Startup: Slightly slower

### Release Configuration
- Optimizations: Enabled
- Debug symbols: Optional
- File size: Smaller (~2-3 MB)
- Startup: Optimized

## Troubleshooting Build Issues

### Error: "frmsaccheck.sln not found"
```powershell
# Ensure you're in the correct directory
cd D:\code\frmsaccheck\
ls frmsaccheck.sln  # Should list the file
```

### Error: ".NET Framework 3.5 not installed"
- Install .NET Framework 3.5 from Windows Features
- Or download from: https://dotnet.microsoft.com/download/dotnet-framework/net35-sp1

### Error: "frmsaccheck.csproj: not found"
- Ensure all project files are present
- Check `.gitignore` didn't exclude necessary files

### Build hangs or times out
- Close other Visual Studio instances
- Clear NuGet cache: `nuget locals all -clear`
- Restart Visual Studio

## Project Dependencies

### NuGet Packages
None required for .NET Framework 3.5 (uses built-in libraries)

### System References
- System
- System.Windows.Forms
- System.Drawing
- System.Diagnostics
- System.Security.Principal
- Microsoft.Win32 (Registry)

## Post-Build Steps

After successful build:

1. **Test the application**
   ```bash
   bin\Release\frmsaccheck.exe
   ```

2. **Verify functionality**
   - Check .NET 3.5 detection
   - Verify SAC status display
   - Test Enable/Disable buttons
   - Check Dark Mode rendering

3. **Prepare for release**
   - Create release folder
   - Copy `.exe` and `.manifest` files
   - Include `README.md`
   - Generate checksums (optional)

## Creating Releases

### Manual Release

```bash
# Create release directory
mkdir releases\v1.0.0

# Copy executable and manifest
copy bin\Release\frmsaccheck.exe releases\v1.0.0\
copy app.manifest releases\v1.0.0\frmsaccheck.exe.manifest

# Create ZIP archive
powershell Compress-Archive -Path releases\v1.0.0\* -DestinationPath releases\v1.0.0.zip
```

### GitHub Release

1. Push code to GitHub
2. Create new Release tag
3. Upload `frmsaccheck.exe` and `app.manifest`
4. Add release notes

## Continuous Integration

For automated builds, use GitHub Actions:

```yaml
# .github/workflows/build.yml
name: Build

on: [push, pull_request]

jobs:
  build:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v2
      - name: Setup .NET Framework
        uses: microsoft/setup-msbuild@v1
      - name: Build
        run: msbuild frmsaccheck.sln /p:Configuration=Release
      - name: Upload artifacts
        uses: actions/upload-artifact@v2
        with:
          name: frmsaccheck
          path: bin/Release/frmsaccheck.exe
```

## Clean Build

To remove all build artifacts and start fresh:

```bash
# PowerShell
Remove-Item -Recurse -Force bin, obj

# Command Prompt
rmdir /s /q bin obj
```

Then rebuild:

```bash
msbuild frmsaccheck.sln /p:Configuration=Release /t:Rebuild
```

## Performance Optimization

For Release builds:

```xml
<!-- In .csproj file -->
<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
  <DebugType>embedded</DebugType>
  <DebugSymbols>true</DebugSymbols>
  <Optimize>true</Optimize>
</PropertyGroup>
```

## Support

For build issues:
- Check Visual Studio version compatibility
- Verify .NET Framework installation
- Review error logs in Output window
- Create issue on GitHub with build output
