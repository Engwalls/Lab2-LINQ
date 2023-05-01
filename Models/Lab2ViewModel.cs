using System.ComponentModel;

namespace Lab2Test.Models
{
    public class Lab2ViewModel
    {
        public int StudentId { get; set; }
        public string StudFirstName { get; set; }
        public string StudLastName { get; set; }
        [DisplayName("Student")]
        public string StudFullName => $"{StudFirstName} {StudLastName}"; 

        public int TeacherId { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        [DisplayName("Teacher")]
        public string TeacherFullName => $"{TeacherFirstName} {TeacherLastName}";


        [DisplayName("Subject ID")]
        public int CourseId { get; set; }
        [DisplayName("Subject")]
        public string CourseName { get; set; }

        [DisplayName("Class ID")]
        public int ClassId { get; set; }
        [DisplayName("Class")]
        public string ClassName { get; set; }
    }
}
