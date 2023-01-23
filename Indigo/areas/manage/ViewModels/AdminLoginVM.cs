using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Indigo.areas.manage.ViewModels
{
    public class AdminLoginVM
    {
        [Required]
        [StringLength(maximumLength:50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 50,MinimumLength =8),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
