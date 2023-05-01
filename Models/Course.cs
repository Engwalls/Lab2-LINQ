using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Lab2Test.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Course ID")]
        public int CourseId { get; set; }
        [DisplayName("Subject")]
        [MaxLength(50)]
        public string CourseName { get; set; }

        [ForeignKey("Students")]
        [DisplayName("Student ID")]
        public int FK_StudentId { get; set; }
        public virtual Student? Students { get; set; }

        [ForeignKey("Teachers")]
        [DisplayName("Teacher ID")]
        public int FK_TeacherId { get; set; }
        public virtual Teacher? Teachers { get; set; }

        [ForeignKey("Classes")]
        [DisplayName("Class ID")]
        public int FK_ClassId { get; set; }
        public virtual Class? Classes { get; set; }
    }
}
