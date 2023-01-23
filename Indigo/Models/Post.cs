using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace Indigo.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        public string Desc { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Tittle { get; set; }
        
        [StringLength(maximumLength: 100)]
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
