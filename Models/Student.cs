using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2Test.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Student ID")]
        public int StudentId { get; set; } = 0;
        [DisplayName ("Student first name")]
        [MaxLength (50)]
        public string StudFirstName { get; set; }
        [DisplayName ("Student last name")]
        [MaxLength (50)]
        public string StudLastName { get; set; }
        [DisplayName("Student")]
        public string StudFullName => $"{StudFirstName} {StudLastName}";
        public virtual ICollection<Course>? Courses { get; set; }
    }
}
