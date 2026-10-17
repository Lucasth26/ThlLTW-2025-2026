// Author: NHH

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreLA7NHH.Models
{
    [Table("Banner")]
    public class NHHBanner
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(100, ErrorMessage = "Tên tối đa 100 ký tự")]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Trạng thái")]
        [Range(0, 255, ErrorMessage = "Trạng thái không hợp lệ")]
        public byte Status { get; set; } = 1;

        [Display(Name = "Độ ưu tiên")]
        [Range(0, int.MaxValue, ErrorMessage = "Độ ưu tiên phải lớn hơn hoặc bằng 0")]
        public int Priority { get; set; } = 0;

        [Display(Name = "Hình ảnh")]
        [StringLength(100)]
        public string Image { get; set; }

        [Display(Name = "Hình ảnh")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Mô tả")]
        [StringLength(350, ErrorMessage = "Mô tả tối đa 350 ký tự")]
        public string Description { get; set; }
    }
}
