using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public class UserRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "UserName is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "UserName characters quantity is between 2 to 50.")]
        public string Username { get; set; }
    }
}
