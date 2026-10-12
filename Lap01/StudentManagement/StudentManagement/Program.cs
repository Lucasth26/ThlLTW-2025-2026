using StudentManagement.Managers;
using StudentManagement.Models;
using StudentManagement.Services;
using StudentManagement.Views;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var service = new StudentService();
SeedDuLieuMau(service);

var view = new StudentConsoleView();
var menuManager = new MenuManager(service, view);
menuManager.Chay();

Console.WriteLine("Cảm ơn đã sử dụng chương trình. Tạm biệt!");

static void SeedDuLieuMau(StudentService service)
{
    service.ThemSinhVien("SV001", "Trần Hữu Long", new DateTime(2003, 5, 12), GioiTinh.Nam,
        "Công nghệ thông tin", 8.7, TrangThaiHocTap.DangHoc, "long.tran@example.com", "0901234567");

    service.ThemSinhVien("SV002", "Nguyễn Thị Hoa", new DateTime(2002, 11, 3), GioiTinh.Nu,
        "Kế toán", 7.5, TrangThaiHocTap.DangHoc, "hoa.nguyen@example.com", null);

    service.ThemSinhVien("SV003", "Lê Văn Minh", new DateTime(2001, 2, 20), GioiTinh.Nam,
        "Công nghệ thông tin", 9.1, TrangThaiHocTap.DaTotNghiep, null, "0912345678");

    service.ThemSinhVien("SV004", "Phạm Thị Lan", new DateTime(2003, 8, 15), GioiTinh.Nu,
        "Quản trị kinh doanh", 6.8, TrangThaiHocTap.BaoLuu, "lan.pham@example.com", "0987654321");
}
