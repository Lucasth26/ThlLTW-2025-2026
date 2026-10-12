using System.Text.RegularExpressions;
using StudentManagement.Models;

namespace StudentManagement.Validators;

/// <summary>
/// Kiểm tra các quy tắc nghiệp vụ liên quan đến sinh viên:
/// - Mã sinh viên không được trùng (kiểm tra dựa trên danh sách hiện có).
/// - Họ tên không được rỗng.
/// - Điểm trung bình nằm trong khoảng 0 - 10.
/// - Email phải đúng định dạng (nếu có nhập).
/// </summary>
public static class StudentValidator
{
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled);

    public static ValidationResult KiemTraMaSV(string? maSV, IEnumerable<Student> danhSachHienCo, string? maSVBoQua = null)
    {
        if (string.IsNullOrWhiteSpace(maSV))
            return ValidationResult.ThatBai("Mã sinh viên không được để trống.");

        bool bịTrùng = danhSachHienCo.Any(sv =>
            sv.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase) &&
            !sv.MaSV.Equals(maSVBoQua, StringComparison.OrdinalIgnoreCase));

        return bịTrùng
            ? ValidationResult.ThatBai($"Mã sinh viên '{maSV}' đã tồn tại.")
            : ValidationResult.ThanhCong();
    }

    public static ValidationResult KiemTraHoTen(string? hoTen)
    {
        return string.IsNullOrWhiteSpace(hoTen)
            ? ValidationResult.ThatBai("Họ tên không được để trống.")
            : ValidationResult.ThanhCong();
    }

    public static ValidationResult KiemTraDiemTrungBinh(double diem)
    {
        return diem is < 0 or > 10
            ? ValidationResult.ThatBai("Điểm trung bình phải nằm trong khoảng từ 0 đến 10.")
            : ValidationResult.ThanhCong();
    }

    public static ValidationResult KiemTraEmail(string? email)
    {
        // Email là tùy chọn (nullable) — chỉ kiểm tra định dạng khi người dùng có nhập.
        if (string.IsNullOrWhiteSpace(email))
            return ValidationResult.ThanhCong();

        return EmailRegex.IsMatch(email)
            ? ValidationResult.ThanhCong()
            : ValidationResult.ThatBai($"Email '{email}' không đúng định dạng.");
    }

    public static ValidationResult KiemTraNgaySinh(DateTime ngaySinh)
    {
        return ngaySinh >= DateTime.Today
            ? ValidationResult.ThatBai("Ngày sinh phải là một ngày trong quá khứ.")
            : ValidationResult.ThanhCong();
    }

    /// <summary>
    /// Kiểm tra toàn bộ thông tin sinh viên trước khi thêm mới.
    /// Trả về danh sách tất cả lỗi tìm thấy (rỗng nếu hợp lệ).
    /// </summary>
    public static List<string> KiemTraKhiThem(
        string? maSV, string? hoTen, DateTime ngaySinh, double diemTrungBinh,
        string? email, IEnumerable<Student> danhSachHienCo)
    {
        var loiList = new List<string>();

        var ketQua = new[]
        {
            KiemTraMaSV(maSV, danhSachHienCo),
            KiemTraHoTen(hoTen),
            KiemTraNgaySinh(ngaySinh),
            KiemTraDiemTrungBinh(diemTrungBinh),
            KiemTraEmail(email)
        };

        foreach (var kq in ketQua)
        {
            if (!kq.HopLe && kq.ThongBaoLoi is not null)
                loiList.Add(kq.ThongBaoLoi);
        }

        return loiList;
    }

    /// <summary>
    /// Kiểm tra thông tin khi cập nhật (bỏ qua kiểm tra trùng với chính mã đang cập nhật).
    /// </summary>
    public static List<string> KiemTraKhiCapNhat(
        string maSVDangSua, string? hoTen, DateTime ngaySinh, double diemTrungBinh,
        string? email, IEnumerable<Student> danhSachHienCo)
    {
        var loiList = new List<string>();

        var ketQua = new[]
        {
            KiemTraHoTen(hoTen),
            KiemTraNgaySinh(ngaySinh),
            KiemTraDiemTrungBinh(diemTrungBinh),
            KiemTraEmail(email)
        };

        foreach (var kq in ketQua)
        {
            if (!kq.HopLe && kq.ThongBaoLoi is not null)
                loiList.Add(kq.ThongBaoLoi);
        }

        return loiList;
    }
}
