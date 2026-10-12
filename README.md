# BÀI 1 – QUẢN LÝ SINH VIÊN BẰNG OOP

## Thông tin sinh viên

| Thông tin      | Chi tiết              |
|----------------|------------------------|
| Họ tên         | Trần Hữu Long          |
| Lớp            | CNTT2K65               |
| Mã số sinh viên| 241230780               |
| Bài tập        | C# .NET 8 Console App – Bài 1: Quản lý sinh viên bằng OOP |

## Mô tả

Ứng dụng console quản lý sinh viên, áp dụng Class/Object, Constructor, Property,
Encapsulation, Static Member, Nullable Reference Types và `List<T>`.

## Cấu trúc thư mục

```
StudentManagement/
├── StudentManagement.sln
├── StudentManagement.csproj
├── Program.cs                     # Entry point, khởi tạo dữ liệu mẫu và chạy menu
├── README.md
├── Data/
│   └── du-lieu-mau.csv            # Dữ liệu mẫu
├── Models/
│   ├── Student.cs                 # Class Student (encapsulation, static member)
│   ├── GioiTinh.cs                # Enum giới tính
│   └── TrangThaiHocTap.cs         # Enum trạng thái học tập
├── Validators/
│   ├── StudentValidator.cs        # Kiểm tra quy tắc nghiệp vụ
│   └── ValidationResult.cs        # Kết quả kiểm tra hợp lệ
├── Services/
│   └── StudentService.cs          # Nghiệp vụ CRUD, tìm kiếm, sắp xếp, thống kê
├── Views/
│   └── StudentConsoleView.cs      # Nhập/xuất dữ liệu qua console
└── Managers/
    └── MenuManager.cs             # Điều phối vòng lặp menu chính
```

## Cách chạy chương trình

```bash
cd StudentManagement
dotnet restore
dotnet run
```

## Danh sách chức năng đã hoàn thành

1. Thêm sinh viên
2. Hiển thị danh sách
3. Tìm sinh viên theo mã
4. Tìm gần đúng theo họ tên
5. Cập nhật sinh viên
6. Xóa sinh viên
7. Sắp xếp theo họ tên
8. Sắp xếp theo điểm trung bình
9. Hiển thị sinh viên có điểm từ 8 trở lên
10. Hiển thị sinh viên có điểm cao nhất
11. Tính điểm trung bình toàn bộ sinh viên
12. Thống kê sinh viên theo ngành
13. Thống kê sinh viên theo trạng thái

## Danh sách chức năng chưa hoàn thành

- Chưa lưu/đọc dữ liệu ra file JSON hoặc CSV khi khởi động/thoát chương trình (chương trình
  hiện chỉ nạp dữ liệu mẫu vào bộ nhớ khi khởi động — nội dung này thuộc phạm vi Bài 4).
- Chưa có unit test (xUnit) — sẽ bổ sung khi học đến phần kiểm thử.

## Quy tắc nghiệp vụ đã áp dụng

- Mã sinh viên không được trùng (kiểm tra không phân biệt hoa/thường).
- Họ tên không được để trống.
- Điểm trung bình phải nằm trong khoảng 0 – 10.
- Email phải đúng định dạng nếu có nhập (email là trường tùy chọn, dùng `string?`).
- Chỉ được cập nhật hoặc xóa khi sinh viên đã tồn tại trong danh sách.
