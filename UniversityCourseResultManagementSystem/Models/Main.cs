using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityCourseResultManagementSystem.Models
{
    [Table("Course")]
    public class Course
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Course Code is Mandatory Field")]
            [Display(Name = "Code")]
            [StringLength(100, MinimumLength = 5, ErrorMessage = "Course Code Must be 5 characters long")]
            [Remote("IsUniqueCode", "Course", ErrorMessage = "Code already exists!")]
            public string CourseCode { get; set; }

            [Required(ErrorMessage = "Course Name is Mandatory Field")]
            [Display(Name = "Name")]
            [Remote("IsUniqueName", "Course", ErrorMessage = "Course Name already exists!")]
            public string CourseName { get; set; }

            [Required]
            [Display(Name = "Credit")]
            [Range(0.5, 5.0, ErrorMessage = "Credit must be within 0.5 to 5.0")]
            public double CourseCredit { get; set; }

            [Required]
            [Display(Name = "Description")]
            [DataType(DataType.MultilineText)]
            public string CourseDescription { get; set; }

            //  [NotMapped]
            public string CourseAssignTo { get; set; }

            // [NotMapped]
            public bool CourseStatus { get; set; }

            [Display(Name = "Department")]
            public int DepartmentId { get; set; }

            [ForeignKey("DepartmentId")]
            public virtual Department Department { get; set; }

            [Display(Name = "Semester")]
            public int SemesterId { get; set; }

            [ForeignKey("SemesterId")]
            public virtual Semester Semester { get; set; }
        }
    [Table("Department")]
    public class Department
        {
            [Required]
            public int Id { get; set; }

            [Required(ErrorMessage = "Department Code is Mandatory Field")]
            [Display(Name = "Code")]
            [StringLength(7, MinimumLength = 2, ErrorMessage = "Departmrnt Code Must be 2 to  7 characters long")]
            [Remote("IsDeptCodeExists", "Department", ErrorMessage = "Code already exists!")]
            public string DeptCode { get; set; }

            [Required(ErrorMessage = "Department Name is Mandatory Field")]
            [Display(Name = "Name")]
            [Remote("IsDeptNameExists", "Department", ErrorMessage = "Department Name already exists!")]
            public string DeptName { get; set; }
        }
    [Table("Teacher")]
    public class Teacher
        {
            public int Id { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string TeacherName { get; set; }

            [Required]
            [Display(Name = "Address")]
            [DataType(DataType.MultilineText)]
            public string TeacherAddress { get; set; }

            [Required]
            [Display(Name = "Email")]
            [EmailAddress(ErrorMessage = "Invalid Email")]
            [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
            [Remote("IsEmailExists", "Teacher", ErrorMessage = "Email already exists!")]
            public string TeacherEmail { get; set; }

            [Required]
            [Display(Name = "Contact No.")]
            [Phone]
            [StringLength(14, MinimumLength = 7, ErrorMessage = "Invalid Phone Number")]
            public string TeacherContactNo { get; set; }

            [Display(Name = "Designation")]
            public int DesignationId { get; set; }
            public virtual Designation Designation { get; set; }

            [Display(Name = "Department")]
            public int DepartmentId { get; set; }

            [ForeignKey("DepartmentId")]
            public virtual Department Department { get; set; }

            [Required]
            [Range(0, (double)decimal.MaxValue, ErrorMessage = "Credit Must be Non Negative Number")]
            [Display(Name = "Credit to be Taken")]
            public double CreditTaken { get; set; }
            public double CreditLeft { get; set; }
        }
    [Table("Day")]
    public class Day
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    [Table("Student")]
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Remote("IsEmailExists", "Student", ErrorMessage = "Email already exists!")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email")]
        public string Email { set; get; }

        [Required(ErrorMessage = "The Field Contact No is Mandatory.")]
        [Phone(ErrorMessage = "Contact No format is Incorrect.")]
        [StringLength(14, MinimumLength = 7, ErrorMessage = "Invalid Phone Number")]
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "The Field Date is Mandatory.")]
        public DateTime Date { get; set; } //Current date will be default date

        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "The Field Department is Mandatory.")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public string StudentRegNo { get; set; } // Not Mapped in View. It will be Auto Generated
    }
    [Table("Semester")]
    public class Semester
    {
        public int Id { get; set; }

        public string SemesterName { get; set; }
    }
    [Table("Room")]
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    [Table("Grade")]
    public class Grade
    {
        public int Id { get; set; }
        public string GradeName { get; set; }
    }
    [Table("Designation")]
    public class Designation
    {
        public int Id { get; set; }
        public string DesignationName { get; set; }
    }
    [Table("EnrollCourse")]
    public class EnrollCourse
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Select a Student")]
        [Display(Name = "Student Reg. No.")]
        public string RegistrationNo { get; set; } // Need To Check
        public virtual Student Student { get; set; }

        [Required(ErrorMessage = "Please Select a Course")]
        [Display(Name = "Select Course")]

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Required(ErrorMessage = "Provide a Date Please !")]
        [Display(Name = "Date")]
        public DateTime EnrollDate { get; set; }

        public string CourseGrade { get; set; }

    }
    [Table("CourseAssign")]
    public class CourseAssign
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Select a Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required(ErrorMessage = "Please Select a Teacher")]
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        [Display(Name = "Credit to be Taken")]
        [Editable(false)]
        [DefaultValue(0.0)]
        public double CreditTaken { get; set; }

        [Display(Name = "Remaining Credit")]
        [Editable(false)]
        [DefaultValue(0.0)]
        public double CreditLeft { get; set; }

        [Required(ErrorMessage = "Please Select a Course")]
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }


        public string Name { get; set; }

        public double Credit { get; set; }
    }
    [Table("ClassRoomAllocation")]
    public class ClassRoomAllocation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Select a Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required(ErrorMessage = "Please Select a Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Required(ErrorMessage = "Please Select a Room")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        [Required(ErrorMessage = "Please Select a Day")]
        public int DayId { get; set; }
        public virtual Day Day { get; set; }

        [Required(ErrorMessage = "Please Select Start Time")]
        public double StartTime { get; set; }
        [Required(ErrorMessage = "Please Select End Time")]
        public double EndTime { get; set; }

        public string RoomStatus { get; set; }
    }
    public class ClassSchedule
    {


        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Schedule { get; set; }
        public ClassSchedule(string courseCode, string courseName, string schedule)
        {
            CourseCode = courseCode;
            CourseName = courseName;
            Schedule = schedule;
        }
    }
}

