using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindHeartCharity.Models.Domain
{
    public class Post
    {
        [Key]
        [Required]
        public Guid PostId { get; set; }

        [Required]
        public string Content { get; set; }

        public string? Description { get; set; }

        public string? PostImageURL { get; set; }

        public DateTime PostDate { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
