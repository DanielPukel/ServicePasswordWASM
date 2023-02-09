using System.ComponentModel.DataAnnotations;

namespace ServicePassword.BlazorWebAssembly.Models
{
    public class UserObject
    {
        [Required(ErrorMessage = "Please input your email address")]
        public string email { get; set; }
        [Required(ErrorMessage = "Please input your password")]
        [MinLength(6, ErrorMessage = "Password must be 6 characters long")]
        public string password { get; set; }
        [CompareProperty("password", ErrorMessage = "Passwords do not match!")]
        public string passwordAgain { get; set; }
        public string username { get; set; }
    }
    public class UserObjectLogin
    {
        [Required(ErrorMessage = "Please input your email address")]
        public string email { get; set; }
        [Required(ErrorMessage = "Please input your password")]
        public string password { get; set; }
        //[CompareProperty("password", ErrorMessage = "Passwords do not match!")]
    }
}
