using StudentManagement.Models;
using StudentManagement.Services;
using StudentManagement.Views;

namespace StudentManagement.Managers;

/// <summary>
/// Điều khiển vòng lặp menu chính của ứng dụng, nối kết StudentService (nghiệp vụ)
/// với StudentConsoleView (giao diện console).
/// </summary>
public class MenuManager
{
    private readonly StudentService _service;
    private readonly StudentConsoleView _view;
    private bool _dangChay = true;

    public MenuManager(StudentService service, StudentConsoleView view)
    {
        _service = service;
        _view = view;
    }

    public void Chay()
    {
        while (_dangChay)
        {
            HienThiMenu();
            string luaChon = _view.DocChuoi("Chọn chức năng: ");
            XuLyLuaChon(luaChon);
        }
    }

    private void HienThiMenu()
    {
        Console.Clear();
        Console.WriteLine("==================== QUẢN LÝ SINH VIÊN ====================");
        Console.WriteLine(" 1.  Thêm sinh viên");
        Console.WriteLine(" 2.  Hiển thị danh sách");
        Console.WriteLine(" 3.  Tìm sinh viên theo mã");
        Console.WriteLine(" 4.  Tìm gần đúng theo họ tên");
        Console.WriteLine(" 5.  Cập nhật sinh viên");
        Console.WriteLine(" 6.  Xóa sinh viên");
        Console.WriteLine(" 7.  Sắp xếp theo họ tên");
        Console.WriteLine(" 8.  Sắp xếp theo điểm trung bình");
        Console.WriteLine(" 9.  Hiển thị sinh viên có điểm từ 8 trở lên");
        Console.WriteLine("10.  Hiển thị sinh viên có điểm cao nhất");
        Console.WriteLine("11.  Tính điểm trung bình toàn bộ sinh viên");
        Console.WriteLine("12.  Thống kê sinh viên theo ngành");
        Console.WriteLine("13.  Thống kê sinh viên theo trạng thái");
        Console.WriteLine(" 0.  Thoát");
        Console.WriteLine("============================================================");
    }

    private void XuLyLuaChon(string luaChon)
    {
        switch (luaChon)
        {
            case "1": ThemSinhVien(); break;
            case "2": HienThiDanhSach(); break;
            case "3": TimTheoMa(); break;
            case "4": TimGanDungTheoHoTen(); break;
            case "5": CapNhatSinhVien(); break;
            case "6": XoaSinhVien(); break;
            case "7": SapXepTheoHoTen(); break;
            case "8": SapXepTheoDiem(); break;
            case "9": LocSinhVienGioi(); break;
            case "10": SinhVienDiemCaoNhat(); break;
            case "11": DiemTrungBinhToanBo(); break;
            case "12": ThongKeTheoNganh(); break;
            case "13": ThongKeTheoTrangThai(); break;
            case "0": _dangChay = false; break;
            default:
                _view.HienThiCanhBao("Lựa chọn không hợp lệ.");
                _view.ChoDungLai();
                break;
        }
    }

    private void ThemSinhVien()
    {
        _view.HienThiTieuDe("THÊM SINH VIÊN");

        string maSV = _view.DocChuoi("Mã sinh viên: ");
        string hoTen = _view.DocChuoi("Họ tên: ");
        DateTime ngaySinh = _view.DocNgay("Ngày sinh");
        GioiTinh gioiTinh = _view.DocEnum<GioiTinh>("Giới tính:");
        string nganhHoc = _view.DocChuoi("Ngành học: ");
        double diem = _view.DocSo("Điểm trung bình: ");
        TrangThaiHocTap trangThai = _view.DocEnum<TrangThaiHocTap>("Trạng thái học tập:");
        string? email = _view.DocChuoiTuyChon("Email (có thể bỏ trống): ");
        string? sdt = _view.DocChuoiTuyChon("Số điện thoại (có thể bỏ trống): ");

        var (thanhCong, loi) = _service.ThemSinhVien(maSV, hoTen, ngaySinh, gioiTinh, nganhHoc, diem, trangThai, email, sdt);

        if (thanhCong)
            _view.HienThiThongBao($"Đã thêm sinh viên '{hoTen}' thành công.");
        else
            _view.HienThiLoi(loi);

        _view.ChoDungLai();
    }

    private void HienThiDanhSach()
    {
        _view.HienThiTieuDe("DANH SÁCH SINH VIÊN");
        _view.HienThiDanhSach(_service.LayTatCa());
        _view.ChoDungLai();
    }

    private void TimTheoMa()
    {
        _view.HienThiTieuDe("TÌM SINH VIÊN THEO MÃ");
        string maSV = _view.DocChuoi("Nhập mã sinh viên: ");
        var sv = _service.TimTheoMa(maSV);
        if (sv is null)
            _view.HienThiCanhBao($"Không tìm thấy sinh viên có mã '{maSV}'.");
        else
            _view.HienThiDanhSach(new[] { sv });
        _view.ChoDungLai();
    }

    private void TimGanDungTheoHoTen()
    {
        _view.HienThiTieuDe("TÌM SINH VIÊN GẦN ĐÚNG THEO HỌ TÊN");
        string tuKhoa = _view.DocChuoi("Nhập từ khóa họ tên: ");
        var ketQua = _service.TimGanDungTheoHoTen(tuKhoa);
        _view.HienThiDanhSach(ketQua);
        _view.ChoDungLai();
    }

    private void CapNhatSinhVien()
    {
        _view.HienThiTieuDe("CẬP NHẬT SINH VIÊN");
        string maSV = _view.DocChuoi("Nhập mã sinh viên cần cập nhật: ");
        var svHienTai = _service.TimTheoMa(maSV);
        if (svHienTai is null)
        {
            _view.HienThiCanhBao($"Không tìm thấy sinh viên có mã '{maSV}'.");
            _view.ChoDungLai();
            return;
        }

        _view.HienThiDanhSach(new[] { svHienTai });
        _view.HienThiCanhBao("Nhập thông tin mới (Mã SV giữ nguyên):");

        string hoTen = _view.DocChuoi("Họ tên: ");
        DateTime ngaySinh = _view.DocNgay("Ngày sinh");
        GioiTinh gioiTinh = _view.DocEnum<GioiTinh>("Giới tính:");
        string nganhHoc = _view.DocChuoi("Ngành học: ");
        double diem = _view.DocSo("Điểm trung bình: ");
        TrangThaiHocTap trangThai = _view.DocEnum<TrangThaiHocTap>("Trạng thái học tập:");
        string? email = _view.DocChuoiTuyChon("Email (có thể bỏ trống): ");
        string? sdt = _view.DocChuoiTuyChon("Số điện thoại (có thể bỏ trống): ");

        var (thanhCong, loi) = _service.CapNhatSinhVien(maSV, hoTen, ngaySinh, gioiTinh, nganhHoc, diem, trangThai, email, sdt);

        if (thanhCong)
            _view.HienThiThongBao("Cập nhật thành công.");
        else
            _view.HienThiLoi(loi);

        _view.ChoDungLai();
    }

    private void XoaSinhVien()
    {
        _view.HienThiTieuDe("XÓA SINH VIÊN");
        string maSV = _view.DocChuoi("Nhập mã sinh viên cần xóa: ");
        bool ketQua = _service.XoaSinhVien(maSV);
        if (ketQua)
            _view.HienThiThongBao($"Đã xóa sinh viên có mã '{maSV}'.");
        else
            _view.HienThiCanhBao($"Không tìm thấy sinh viên có mã '{maSV}'.");
        _view.ChoDungLai();
    }

    private void SapXepTheoHoTen()
    {
        _view.HienThiTieuDe("DANH SÁCH SẮP XẾP THEO HỌ TÊN (A-Z)");
        _view.HienThiDanhSach(_service.SapXepTheoHoTen());
        _view.ChoDungLai();
    }

    private void SapXepTheoDiem()
    {
        _view.HienThiTieuDe("DANH SÁCH SẮP XẾP THEO ĐIỂM TRUNG BÌNH (GIẢM DẦN)");
        _view.HienThiDanhSach(_service.SapXepTheoDiemTrungBinh(tangDan: false));
        _view.ChoDungLai();
    }

    private void LocSinhVienGioi()
    {
        _view.HienThiTieuDe("SINH VIÊN CÓ ĐIỂM TỪ 8 TRỞ LÊN");
        _view.HienThiDanhSach(_service.LocSinhVienGioi());
        _view.ChoDungLai();
    }

    private void SinhVienDiemCaoNhat()
    {
        _view.HienThiTieuDe("SINH VIÊN CÓ ĐIỂM CAO NHẤT");
        _view.HienThiDanhSach(_service.SinhVienDiemCaoNhat());
        _view.ChoDungLai();
    }

    private void DiemTrungBinhToanBo()
    {
        _view.HienThiTieuDe("ĐIỂM TRUNG BÌNH TOÀN BỘ SINH VIÊN");
        Console.WriteLine($"Điểm trung bình chung: {_service.DiemTrungBinhToanBo():F2}");
        _view.ChoDungLai();
    }

    private void ThongKeTheoNganh()
    {
        _view.HienThiTieuDe("THỐNG KÊ SINH VIÊN THEO NGÀNH");
        foreach (var (nganh, soLuong) in _service.ThongKeTheoNganh())
            Console.WriteLine($"  {nganh,-20}: {soLuong} sinh viên");
        _view.ChoDungLai();
    }

    private void ThongKeTheoTrangThai()
    {
        _view.HienThiTieuDe("THỐNG KÊ SINH VIÊN THEO TRẠNG THÁI");
        foreach (var (trangThai, soLuong) in _service.ThongKeTheoTrangThai())
            Console.WriteLine($"  {trangThai,-15}: {soLuong} sinh viên");
        _view.ChoDungLai();
    }
}
