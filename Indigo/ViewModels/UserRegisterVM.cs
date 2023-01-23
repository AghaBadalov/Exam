using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Indigo.ViewModels
{
    public class UserRegisterVM
    {
        [Required]
        [StringLength(maximumLength: 50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string FullName { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 50,MinimumLength =8),DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 8), DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RepeatPassword { get; set; }
    }
}
