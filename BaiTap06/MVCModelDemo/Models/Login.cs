using System.ComponentModel.DataAnnotations;

namespace MVCModelDemo.Models
{
    public class Login
    {
        public string userName { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
