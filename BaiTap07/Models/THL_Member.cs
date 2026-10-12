using System.ComponentModel.DataAnnotations;

namespace lesson7.Models
{
    public class THL_Member
    {
        [Display(Name = "Mã thành viên")]
        public int Id { get; set; }

        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        [StringLength(20, MinimumLength = 3,
            ErrorMessage = "Tài khoản có độ dài trong khoảng 3 đến 20 ký tự")]
        public string Username { get; set; } 

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8,
            ErrorMessage = "Mật khẩu tối thiểu 8 ký tự")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage = "Điện thoại không được để trống")]
        [RegularExpression(
            @"^0\d{9,12}$",
            ErrorMessage = "Điện thoại phải là 10 ký tự số bắt đầu bằng số 0")]
        public string Phone { get; set; } = string.Empty;
    }
}