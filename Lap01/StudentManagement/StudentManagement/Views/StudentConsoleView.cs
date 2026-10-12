using StudentManagement.Models;

namespace StudentManagement.Views;

/// <summary>
/// Chịu trách nhiệm toàn bộ việc nhập dữ liệu từ bàn phím và hiển thị kết quả ra console.
/// Tách riêng khỏi StudentService để tuân thủ nguyên tắc phân tách trách nhiệm (SRP).
/// </summary>
public class StudentConsoleView
{
    public void HienThiTieuDe(string tieuDe)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 60));
        Console.WriteLine(tieuDe);
        Console.WriteLine(new string('=', 60));
    }

    public void HienThiLoi(IEnumerable<string> loiList)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        foreach (var loi in loiList)
            Console.WriteLine($"  ✗ {loi}");
        Console.ResetColor();
    }

    public void HienThiThongBao(string thongBao)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(thongBao);
        Console.ResetColor();
    }

    public void HienThiCanhBao(string thongBao)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(thongBao);
        Console.ResetColor();
    }

    public void HienThiDanhSach(IEnumerable<Student> danhSach)
    {
        var list = danhSach.ToList();
        if (list.Count == 0)
        {
            HienThiCanhBao("Không có sinh viên nào để hiển thị.");
            return;
        }

        Console.WriteLine($"{"Mã SV",-8} | {"Họ tên",-25} | {"Ngày sinh",10} | {"Giới tính",-4} | " +
                           $"{"Ngành học",-15} | {"Điểm TB",5} | {"Trạng thái",-12} | {"Email",-22} | {"SĐT"}");
        Console.WriteLine(new string('-', 140));
        foreach (var sv in list)
            Console.WriteLine(sv);
        Console.WriteLine(new string('-', 140));
        Console.WriteLine($"Tổng số: {list.Count} sinh viên.");
    }

    public string DocChuoi(string nhanNhan, bool coTheRong = false)
    {
        while (true)
        {
            Console.Write(nhanNhan);
            string? gtri = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(gtri) || coTheRong)
                return gtri ?? string.Empty;
            HienThiCanhBao("  Giá trị không được để trống, vui lòng nhập lại.");
        }
    }

    public string? DocChuoiTuyChon(string nhanNhan)
    {
        Console.Write(nhanNhan);
        string? gtri = Console.ReadLine();
        return string.IsNullOrWhiteSpace(gtri) ? null : gtri.Trim();
    }

    public double DocSo(string nhanNhan)
    {
        while (true)
        {
            Console.Write(nhanNhan);
            string? gtri = Console.ReadLine();
            if (double.TryParse(gtri, out double ketQua))
                return ketQua;
            HienThiCanhBao("  Giá trị không hợp lệ, vui lòng nhập lại một con số.");
        }
    }

    public DateTime DocNgay(string nhanNhan)
    {
        while (true)
        {
            Console.Write($"{nhanNhan} (dd/MM/yyyy): ");
            string? gtri = Console.ReadLine();
            if (DateTime.TryParseExact(gtri, "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime ketQua))
                return ketQua;
            HienThiCanhBao("  Ngày không hợp lệ, đúng định dạng dd/MM/yyyy.");
        }
    }

    public TEnum DocEnum<TEnum>(string nhanNhan) where TEnum : struct, Enum
    {
        var cacGiaTri = Enum.GetValues<TEnum>();
        while (true)
        {
            Console.WriteLine(nhanNhan);
            for (int i = 0; i < cacGiaTri.Length; i++)
                Console.WriteLine($"  {i + 1}. {cacGiaTri[i]}");
            Console.Write("Chọn số: ");
            string? gtri = Console.ReadLine();
            if (int.TryParse(gtri, out int chiSo) && chiSo >= 1 && chiSo <= cacGiaTri.Length)
                return cacGiaTri[chiSo - 1];
            HienThiCanhBao("  Lựa chọn không hợp lệ, vui lòng nhập lại.");
        }
    }

    public void ChoDungLai()
    {
        Console.WriteLine();
        Console.Write("Nhấn Enter để tiếp tục...");
        Console.ReadLine();
    }
}
