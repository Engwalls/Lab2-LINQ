using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Lab2Test.Models
{
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Class ID")]
        public int ClassId { get; set; } = 0;
        [DisplayName("Class name")]
        [MaxLength(50)]
        public string ClassName { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }
    }
}
