# Setup donate.png Image Resource

## Bước 1: Tạo thư mục Resources (nếu chưa có)

1. Mở project trong Visual Studio
2. Chuột phải vào project → **New Folder**
3. Đặt tên: `Resources`

## Bước 2: Đặt File donate.png

1. Copy file `donate.png` vào folder `D:\code\frmsaccheck\Resources\`
2. Đặt tên file: `donate.png`

## Bước 3: Thêm vào Project

### Cách 1: Thêm qua Visual Studio (Khuyến Khích)

1. Chuột phải vào thư mục **Resources** → **Add** → **Existing Item**
2. Chọn file `donate.png`
3. Sau khi thêm, chuột phải lên file `donate.png`
4. Chọn **Properties**
5. Đặt **Build Action** = `Embedded Resource`
6. Đặt **Copy to Output Directory** = `Copy if newer`

### Cách 2: Thêm qua Properties/Resources.resx

1. Chuột phải vào project → **Properties**
2. Chọn tab **Resources**
3. Click **Add Resource** → **Add Existing File**
4. Chọn `donate.png` từ thư mục `Resources`
5. Visual Studio sẽ tự thêm vào `Properties/Resources.resx`

## Bước 4: Sử dụng Image trong Code

### Nếu dùng Properties/Resources:

```csharp
// Khai báo PictureBox trong Designer hoặc code
PictureBox pbDonate = new PictureBox();
pbDonate.Image = Properties.Resources.donate;  // Tự động mapping
```

### Nếu dùng Embedded Resource:

```csharp
// Load từ assembly resources
pbDonate.Image = Image.FromStream(
    this.GetType().Assembly.GetManifestResourceStream(
        "frmsaccheck.Resources.donate.png"
    )
);
```

## Cấu trúc Folder Sau Setup

```
D:\code\frmsaccheck\
├── Form1.cs
├── Form1.Designer.cs
├── Form1.resx
├── frmnetcheck.cs
├── frmnetcheck.Designer.cs
├── Program.cs
├── Properties\
│   ├── Resources.resx
│   ├── Resources.Designer.cs
│   └── Settings.resx
└── Resources\
    └── donate.png
```

## Kiểm Tra xem Resource đã được thêm chưa

1. Mở file `Properties\Resources.Designer.cs`
2. Tìm property: `public static System.Drawing.Bitmap donate`
3. Nếu có, đã thêm thành công ✅

## Sử dụng Image từ Resource

```csharp
// Trên Form1.Designer.cs (nếu có PictureBox)
this.pbDonate.Image = global::frmsaccheck.Properties.Resources.donate;

// Hoặc trong Code
pbDonate.Image = Properties.Resources.donate;
```

## Troubleshooting

### Lỗi: "Resource not found"
- Kiểm tra tên file (phải là `donate` không có extension)
- Rebuild solution
- Xóa bin/ obj/ và build lại

### Lỗi: "donate is not in namespace"
- Chắc chắn file đã được thêm vào `Properties/Resources.resx`
- Build project: `Ctrl+Shift+B`
- `Properties.Designer.cs` sẽ được auto-generate

### File không hiển thị
- Chắc chắn **Build Action** = `Content` hoặc `Embedded Resource`
- Chắc chắn **Copy to Output** = `Copy if newer` hoặc `Always`
