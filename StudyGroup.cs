using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegisterDI
{
    public class StudyGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Topic { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
