using System.ComponentModel.DataAnnotations;

namespace Indigo.ViewModels
{
    public class UserLoginVM
    {
        [Required]
        [StringLength(maximumLength: 50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 8), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
