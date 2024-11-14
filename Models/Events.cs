using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TextEditor.Models
{
    public class Events
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }

        // New property to track completion
        public bool IsComplete { get; set; }  // True = complete, False = incomplete
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }    }

}
