// Author: NHH

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreLA7NHH.Models
{
    [Table("Blog")]
    public class NHHBlog
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

        [Display(Name = "Ngày tạo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; } = DateTime.Today;

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
