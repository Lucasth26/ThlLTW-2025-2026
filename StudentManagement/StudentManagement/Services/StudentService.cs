using StudentManagement.Models;
using StudentManagement.Validators;

namespace StudentManagement.Services;

/// <summary>
/// Chứa toàn bộ nghiệp vụ quản lý sinh viên: thêm, sửa, xóa, tìm kiếm,
/// sắp xếp, lọc và thống kê. Dữ liệu được lưu trong List&lt;Student&gt;.
/// </summary>
public class StudentService
{
    private readonly List<Student> _danhSachSinhVien = new();

    public IReadOnlyList<Student> LayTatCa() => _danhSachSinhVien.AsReadOnly();

    // 1. Thêm sinh viên
    public (bool ThanhCong, List<string> Loi) ThemSinhVien(
        string maSV, string hoTen, DateTime ngaySinh, GioiTinh gioiTinh,
        string nganhHoc, double diemTrungBinh, TrangThaiHocTap trangThai,
        string? email, string? soDienThoai)
    {
        var loi = StudentValidator.KiemTraKhiThem(maSV, hoTen, ngaySinh, diemTrungBinh, email, _danhSachSinhVien);
        if (loi.Count > 0)
            return (false, loi);

        var sv = new Student(maSV, hoTen, ngaySinh, gioiTinh, nganhHoc, diemTrungBinh, trangThai, email, soDienThoai);
        _danhSachSinhVien.Add(sv);
        return (true, loi);
    }

    // 2. Hiển thị danh sách -> dùng LayTatCa()

    // 3. Tìm sinh viên theo mã
    public Student? TimTheoMa(string maSV)
    {
        return _danhSachSinhVien.FirstOrDefault(sv =>
            sv.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));
    }

    // 4. Tìm gần đúng theo họ tên
    public List<Student> TimGanDungTheoHoTen(string tuKhoa)
    {
        if (string.IsNullOrWhiteSpace(tuKhoa))
            return new List<Student>();

        return _danhSachSinhVien
            .Where(sv => sv.HoTen.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    // 5. Cập nhật sinh viên (chỉ khi tồn tại)
    public (bool ThanhCong, List<string> Loi) CapNhatSinhVien(
        string maSV, string hoTen, DateTime ngaySinh, GioiTinh gioiTinh,
        string nganhHoc, double diemTrungBinh, TrangThaiHocTap trangThai,
        string? email, string? soDienThoai)
    {
        var sv = TimTheoMa(maSV);
        if (sv is null)
            return (false, new List<string> { $"Không tìm thấy sinh viên có mã '{maSV}'." });

        var loi = StudentValidator.KiemTraKhiCapNhat(maSV, hoTen, ngaySinh, diemTrungBinh, email, _danhSachSinhVien);
        if (loi.Count > 0)
            return (false, loi);

        sv.CapNhatThongTin(hoTen, ngaySinh, gioiTinh, nganhHoc, diemTrungBinh, trangThai, email, soDienThoai);
        return (true, loi);
    }

    // 6. Xóa sinh viên (chỉ khi tồn tại)
    public bool XoaSinhVien(string maSV)
    {
        var sv = TimTheoMa(maSV);
        if (sv is null) return false;
        return _danhSachSinhVien.Remove(sv);
    }

    // 7. Sắp xếp theo họ tên
    public List<Student> SapXepTheoHoTen(bool tangDan = true)
    {
        return tangDan
            ? _danhSachSinhVien.OrderBy(sv => sv.HoTen, StringComparer.OrdinalIgnoreCase).ToList()
            : _danhSachSinhVien.OrderByDescending(sv => sv.HoTen, StringComparer.OrdinalIgnoreCase).ToList();
    }

    // 8. Sắp xếp theo điểm trung bình
    public List<Student> SapXepTheoDiemTrungBinh(bool tangDan = true)
    {
        return tangDan
            ? _danhSachSinhVien.OrderBy(sv => sv.DiemTrungBinh).ToList()
            : _danhSachSinhVien.OrderByDescending(sv => sv.DiemTrungBinh).ToList();
    }

    // 9. Hiển thị sinh viên có điểm từ 8 trở lên
    public List<Student> LocSinhVienGioi(double nguong = 8.0)
    {
        return _danhSachSinhVien.Where(sv => sv.DiemTrungBinh >= nguong).ToList();
    }

    // 10. Hiển thị sinh viên có điểm cao nhất
    public List<Student> SinhVienDiemCaoNhat()
    {
        if (_danhSachSinhVien.Count == 0) return new List<Student>();
        double max = _danhSachSinhVien.Max(sv => sv.DiemTrungBinh);
        return _danhSachSinhVien.Where(sv => Math.Abs(sv.DiemTrungBinh - max) < 0.0001).ToList();
    }

    // 11. Tính điểm trung bình toàn bộ sinh viên
    public double DiemTrungBinhToanBo()
    {
        return _danhSachSinhVien.Count == 0 ? 0 : _danhSachSinhVien.Average(sv => sv.DiemTrungBinh);
    }

    // 12. Thống kê sinh viên theo ngành
    public Dictionary<string, int> ThongKeTheoNganh()
    {
        return _danhSachSinhVien
            .GroupBy(sv => sv.NganhHoc)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    // 13. Thống kê sinh viên theo trạng thái
    public Dictionary<TrangThaiHocTap, int> ThongKeTheoTrangThai()
    {
        return _danhSachSinhVien
            .GroupBy(sv => sv.TrangThaiHocTap)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public int TongSoSinhVien => _danhSachSinhVien.Count;
}
