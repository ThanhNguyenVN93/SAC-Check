# GitHub Repository Template

Copy and customize this for your GitHub repository:

```markdown
# Smart App Control Manager

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Windows](https://img.shields.io/badge/Platform-Windows%2011-0078D4?logo=windows)](https://www.microsoft.com/windows)
[![.NET Framework](https://img.shields.io/badge/.NET-Framework%203.5-512BD4?logo=.net)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/Language-C%23-239120?logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)

A modern Windows Forms application to manage Smart App Control (SAC) on Windows 11 Build 22621+.

## Quick Start

1. **Download**: Get the latest release
2. **Run**: Execute with administrator privileges
3. **Manage**: Enable or disable SAC as needed

## Features

- ✅ Real-time SAC status detection
- ✅ One-click enable/disable functionality
- ✅ Auto .NET Framework 3.5 setup
- ✅ Dark Mode Minimalist UI
- ✅ Secure registry operations
- ✅ System compatibility check

## Requirements

- Windows 11 Build 22621 or later
- Administrator privileges
- .NET Framework 3.5

## Documentation

- [README.md](README.md) - Full documentation
- [BUILD.md](BUILD.md) - Build instructions
- [CONTRIBUTING.md](CONTRIBUTING.md) - Contribution guidelines
- [CHANGELOG.md](CHANGELOG.md) - Version history

## License

MIT License - See [LICENSE](LICENSE) file

## Support

For issues, questions, or feature requests, please [create an issue](https://github.com/yourusername/frmsaccheck/issues).
```

## Setup Instructions

### 1. Create Repository on GitHub

1. Go to [GitHub.com](https://github.com)
2. Click **+** → **New repository**
3. Name: `frmsaccheck`
4. Description: `Smart App Control Manager - Windows 11`
5. Visibility: Public/Private (your choice)
6. Initialize with: README (optional - we have one)
7. Click **Create repository**

### 2. Initialize Local Git

```powershell
# Navigate to project directory
cd D:\code\frmsaccheck\

# Initialize git
git init

# Add all files
git add .

# Initial commit
git commit -m "Initial commit: Smart App Control Manager v1.0"

# Add remote (replace with your repo URL)
git remote add origin https://github.com/yourusername/frmsaccheck.git

# Push to GitHub
git branch -M main
git push -u origin main
```

### 3. .gitignore Already Included

The `.gitignore` file in this project covers:
- Build artifacts (bin/, obj/)
- IDE files (.vs/, .idea/)
- User-specific settings
- OS-specific files

## Repository Structure

```
frmsaccheck/
├── .github/
│   └── workflows/          (CI/CD pipelines)
├── bin/                    (ignored)
├── obj/                    (ignored)
├── .gitignore
├── BUILD.md
├── CHANGELOG.md
├── CONTRIBUTING.md
├── LICENSE
├── README.md
├── frmsaccheck.sln
├── frmsaccheck.csproj
├── Program.cs
├── Form1.cs
├── Form1.Designer.cs
├── Form1.resx
├── frmnetcheck.cs
├── frmnetcheck.Designer.cs
├── frmnetcheck.resx
├── app.manifest
└── frmsaccheck.resx
```

## Adding More Files

### Add .github/workflows/build.yml

```yaml
name: Build

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: windows-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup MSBuild
      uses: microsoft/setup-msbuild@v1.1
    
    - name: Build
      run: msbuild frmsaccheck.sln /p:Configuration=Release
    
    - name: Upload Release Build
      uses: actions/upload-artifact@v3
      with:
        name: frmsaccheck-release
        path: bin/Release/frmsaccheck.exe
```

### Add .github/ISSUE_TEMPLATE/bug_report.md

```markdown
---
name: Bug Report
about: Report a bug
---

## Description
Brief description of the bug

## Steps to Reproduce
1. Step 1
2. Step 2
3. Step 3

## Expected Behavior
What should happen

## Actual Behavior
What actually happens

## System Information
- Windows Build: 
- .NET Framework: 
- Application Version: 

## Error Message
(if any)

## Additional Context
(if any)
```

## Publishing Releases

### Create Release on GitHub

1. Go to repository on GitHub
2. Click **Releases** → **Draft a new release**
3. Set tag: `v1.0.0`
4. Title: `Smart App Control Manager v1.0`
5. Upload files:
   - `frmsaccheck.exe`
   - `app.manifest`
6. Add release notes
7. Click **Publish release**

### Release Checklist

- [ ] All tests passed
- [ ] Version bumped in code/changelog
- [ ] Build successful
- [ ] README updated
- [ ] CHANGELOG updated
- [ ] Executable signed (optional)
- [ ] Release notes written
- [ ] Tag created
- [ ] Files uploaded

## GitHub Settings Recommendations

### General
- ✅ Enable wiki
- ✅ Enable discussions
- ✅ Enable projects
- ✅ Default branch: `main`

### Branches
- Protect main branch: Require PR reviews
- Require status checks

### Actions
- ✅ Allow all actions

### Security
- Enable Dependabot if using NuGet packages

## Quick Links

- **Repository**: `https://github.com/yourusername/frmsaccheck`
- **Issues**: `https://github.com/yourusername/frmsaccheck/issues`
- **Releases**: `https://github.com/yourusername/frmsaccheck/releases`
- **Wiki**: `https://github.com/yourusername/frmsaccheck/wiki`

## First Push Commands

```powershell
# One-time setup
git config user.name "Your Name"
git config user.email "your.email@example.com"

# Push everything
git push -u origin main --all --tags

# Verify
git remote -v
git branch -vv
```

## Future Improvements

- [ ] Add Unit Tests
- [ ] Setup CI/CD pipeline
- [ ] Create release workflow
- [ ] Add code coverage badge
- [ ] Setup automatic releases
- [ ] Add Discord/Slack integration
