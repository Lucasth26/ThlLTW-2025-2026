using System.ComponentModel.DataAnnotations;

namespace MVCModelDemo.Models
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}
