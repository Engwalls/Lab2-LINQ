using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Lab2Test.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Teacher ID")]
        public int TeacherId { get; set; } = 0;
        [DisplayName("Teacher first name")]
        [MaxLength(50)]
        public string TeacherFirstName { get; set; }
        [DisplayName("Teacher last name")]
        [MaxLength(50)]
        public string TeacherLastName { get; set; }
        [DisplayName("Teacher")]
        public string TeacherFullName => $"{TeacherFirstName} {TeacherLastName}";
        public virtual ICollection<Course>? Courses { get; set; }
    }
}
