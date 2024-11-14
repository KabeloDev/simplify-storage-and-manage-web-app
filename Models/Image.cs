using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TextEditor.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public DateTime UploadDate { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }
    }
}
