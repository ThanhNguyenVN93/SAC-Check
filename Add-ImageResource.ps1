# Add-ImageResource.ps1
# Script để tự động thêm donate.png vào Properties/Resources.resx

param(
    [string]$ImagePath = "D:\code\frmsaccheck\Resources\donate.png",
    [string]$ProjectPath = "D:\code\frmsaccheck",
    [string]$ResourceName = "donate"
)

# Kiểm tra xem file ảnh tồn tại chưa
if (-not (Test-Path $ImagePath)) {
    Write-Host "❌ Lỗi: File ảnh không tìm thấy tại: $ImagePath" -ForegroundColor Red
    exit 1
}

Write-Host "✓ Tìm thấy file ảnh: $ImagePath" -ForegroundColor Green

# Đường dẫn đến file Resources.resx
$ResourcesResx = Join-Path $ProjectPath "Properties\Resources.resx"

if (-not (Test-Path $ResourcesResx)) {
    Write-Host "❌ Lỗi: File Resources.resx không tìm thấy tại: $ResourcesResx" -ForegroundColor Red
    exit 1
}

Write-Host "✓ Tìm thấy file Resources.resx" -ForegroundColor Green

# Load XML từ Resources.resx
[xml]$resxContent = Get-Content $ResourcesResx

# Kiểm tra xem resource đã tồn tại chưa
$existingResource = $resxContent.root.data | Where-Object { $_.name -eq $ResourceName }

if ($existingResource) {
    Write-Host "⚠️  Resource '$ResourceName' đã tồn tại. Bỏ qua..." -ForegroundColor Yellow
} else {
    # Chuyển file ảnh thành base64
    $imageBytes = [System.IO.File]::ReadAllBytes($ImagePath)
    $base64Image = [System.Convert]::ToBase64String($imageBytes)
    
    # Tạo XML element mới
    $newDataElement = $resxContent.CreateElement("data")
    $newDataElement.SetAttribute("name", $ResourceName)
    $newDataElement.SetAttribute("type", "System.Resources.ResXString, System.Windows.Forms")
    $newDataElement.SetAttribute("mimetype", "application/x-microsoft.net.object.binary.base64")
    
    $valueElement = $resxContent.CreateElement("value")
    $valueElement.InnerText = $base64Image
    $newDataElement.AppendChild($valueElement)
    
    # Thêm vào root
    $resxContent.root.AppendChild($newDataElement)
    
    # Lưu file
    $resxContent.Save($ResourcesResx)
    
    Write-Host "✓ Đã thêm resource '$ResourceName' vào Resources.resx" -ForegroundColor Green
}

# Rebuild project
Write-Host "`n⏳ Rebuild project để cập nhật Designer.cs..." -ForegroundColor Cyan

# Sử dụng MSBuild để rebuild
$msbuildPath = "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe"

if (Test-Path $msbuildPath) {
    $projectFile = Join-Path $ProjectPath "frmsaccheck.csproj"
    
    & $msbuildPath $projectFile /p:Configuration=Debug /t:Rebuild
    
    Write-Host "✓ Build hoàn tất" -ForegroundColor Green
} else {
    Write-Host "⚠️  MSBuild không tìm thấy. Vui lòng rebuild manually trong Visual Studio." -ForegroundColor Yellow
    Write-Host "   Nhấn Ctrl+Shift+B trong Visual Studio để rebuild" -ForegroundColor Cyan
}

Write-Host "`n✅ Hoàn tất! Bây giờ bạn có thể sử dụng:" -ForegroundColor Green
Write-Host "   ptbdonate.Image = Properties.Resources.$ResourceName;" -ForegroundColor Yellow
