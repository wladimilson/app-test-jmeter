using System.ComponentModel.DataAnnotations;

namespace app_test_jmeter.Models
{
    public class LoginViewModel  
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}