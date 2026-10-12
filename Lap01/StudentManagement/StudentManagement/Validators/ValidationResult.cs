namespace StudentManagement.Validators;

/// <summary>
/// Kết quả kiểm tra hợp lệ: cho biết dữ liệu có hợp lệ không và thông báo lỗi (nếu có).
/// </summary>
public class ValidationResult
{
    public bool HopLe { get; }
    public string? ThongBaoLoi { get; }

    private ValidationResult(bool hopLe, string? thongBaoLoi)
    {
        HopLe = hopLe;
        ThongBaoLoi = thongBaoLoi;
    }

    public static ValidationResult ThanhCong() => new(true, null);
    public static ValidationResult ThatBai(string thongBaoLoi) => new(false, thongBaoLoi);
}
