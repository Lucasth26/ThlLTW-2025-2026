namespace StudentManagement.Models;

/// <summary>
/// Đại diện cho một sinh viên trong hệ thống.
/// Áp dụng Encapsulation: các thuộc tính chỉ có thể được thay đổi thông qua
/// constructor hoặc các phương thức public có kiểm soát (Update).
/// </summary>
public class Student
{
    // ---- Static member: đếm tổng số sinh viên đã được tạo ra trong vòng đời chương trình ----
    private static int _soLuongSinhVienDaTao;
    public static int SoLuongSinhVienDaTao => _soLuongSinhVienDaTao;

    public string MaSV { get; private set; }
    public string HoTen { get; private set; }
    public DateTime NgaySinh { get; private set; }
    public GioiTinh GioiTinh { get; private set; }

    // Nullable reference types: Email và SĐT có thể chưa được cung cấp
    public string? Email { get; private set; }
    public string? SoDienThoai { get; private set; }

    public string NganhHoc { get; private set; }
    public double DiemTrungBinh { get; private set; }
    public TrangThaiHocTap TrangThaiHocTap { get; private set; }

    public Student(
        string maSV,
        string hoTen,
        DateTime ngaySinh,
        GioiTinh gioiTinh,
        string nganhHoc,
        double diemTrungBinh,
        TrangThaiHocTap trangThaiHocTap = TrangThaiHocTap.DangHoc,
        string? email = null,
        string? soDienThoai = null)
    {
        MaSV = maSV;
        HoTen = hoTen;
        NgaySinh = ngaySinh;
        GioiTinh = gioiTinh;
        NganhHoc = nganhHoc;
        DiemTrungBinh = diemTrungBinh;
        TrangThaiHocTap = trangThaiHocTap;
        Email = email;
        SoDienThoai = soDienThoai;

        _soLuongSinhVienDaTao++;
    }

    /// <summary>
    /// Cập nhật thông tin sinh viên (không cho phép đổi Mã SV để đảm bảo tính duy nhất).
    /// </summary>
    public void CapNhatThongTin(
        string hoTen,
        DateTime ngaySinh,
        GioiTinh gioiTinh,
        string nganhHoc,
        double diemTrungBinh,
        TrangThaiHocTap trangThaiHocTap,
        string? email,
        string? soDienThoai)
    {
        HoTen = hoTen;
        NgaySinh = ngaySinh;
        GioiTinh = gioiTinh;
        NganhHoc = nganhHoc;
        DiemTrungBinh = diemTrungBinh;
        TrangThaiHocTap = trangThaiHocTap;
        Email = email;
        SoDienThoai = soDienThoai;
    }

    public int Tuoi
    {
        get
        {
            var today = DateTime.Today;
            var tuoi = today.Year - NgaySinh.Year;
            if (NgaySinh.Date > today.AddYears(-tuoi)) tuoi--;
            return tuoi;
        }
    }

    public override string ToString()
    {
        return $"{MaSV,-8} | {HoTen,-25} | {NgaySinh:dd/MM/yyyy} | {GioiTinh,-4} | " +
               $"{NganhHoc,-15} | {DiemTrungBinh,5:F2} | {TrangThaiHocTap,-12} | " +
               $"{Email ?? "(chưa có)",-22} | {SoDienThoai ?? "(chưa có)"}";
    }
}
