# MVC Model Demo

**Họ và tên:** Nguyễn Hữu Hoành
**MSV:** 241230736

## Mô tả

Bài thực hành minh họa Model trong ASP.NET Core MVC, gồm:

- **Models**
  - `User.cs` – model dữ liệu người dùng (Id, name, address, email)
  - `Login.cs` – model dùng để minh họa model binding (username/password)
- **Controllers**
  - `HomeController.cs` – các action method: `Index` (List), `Create`, `Edit`, `Details`, `Delete`, `Login`
- **Views** (`Views/Home`)
  - `Index.cshtml` – hiển thị danh sách User (List template)
  - `Create.cshtml` – form thêm mới User (Create template)
  - `Edit.cshtml` – form chỉnh sửa User (Edit template)
  - `Details.cshtml` – hiển thị chi tiết một User (Details template)
  - `Delete.cshtml` – xác nhận xóa User (Delete template)
  - `Login.cshtml` – form đăng nhập, minh họa model binding

## Cách chạy

1. Tạo project ASP.NET Core MVC mới (hoặc copy các file trên vào project có sẵn).
2. Copy các file trong `Models/`, `Controllers/`, `Views/Home/` vào đúng thư mục tương ứng.
3. Chạy project, truy cập `/Home/Index` để xem danh sách User; từ đó có thể Create/Edit/Details/Delete.
4. Truy cập `/Home/Login`, nhập `userName = Peter`, `password = pass@123` để thấy model binding hoạt động.

## Ghi chú

Demo dùng một `List<User>` lưu trong bộ nhớ (in-memory) thay cho cơ sở dữ liệu thật, để có thể chạy ngay không cần cấu hình DB.
