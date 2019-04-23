namespace UniversityCourseResultManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstOne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassRoomAllocation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                        StartTime = c.Double(nullable: false),
                        EndTime = c.Double(nullable: false),
                        RoomStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId)
                .ForeignKey("dbo.Day", t => t.DayId)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .Index(t => t.DepartmentId)
                .Index(t => t.CourseId)
                .Index(t => t.RoomId)
                .Index(t => t.DayId);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseCode = c.String(nullable: false, maxLength: 100),
                        CourseName = c.String(nullable: false),
                        CourseCredit = c.Double(nullable: false),
                        CourseDescription = c.String(nullable: false),
                        CourseAssignTo = c.String(),
                        CourseStatus = c.Boolean(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        SemesterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .ForeignKey("dbo.Semester", t => t.SemesterId)
                .Index(t => t.DepartmentId)
                .Index(t => t.SemesterId);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeptCode = c.String(nullable: false, maxLength: 7),
                        DeptName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Semester",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SemesterName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Day",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClassSchedule",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseCode = c.String(),
                        CourseName = c.String(),
                        Schedule = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseAssign",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        CreditTaken = c.Double(nullable: false),
                        CreditLeft = c.Double(nullable: false),
                        CourseID = c.Int(nullable: false),
                        Name = c.String(),
                        Credit = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseID)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .ForeignKey("dbo.Teacher", t => t.TeacherId)
                .Index(t => t.DepartmentId)
                .Index(t => t.TeacherId)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherName = c.String(nullable: false),
                        TeacherAddress = c.String(nullable: false),
                        TeacherEmail = c.String(nullable: false),
                        TeacherContactNo = c.String(nullable: false, maxLength: 14),
                        DesignationId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        CreditTaken = c.Double(nullable: false),
                        CreditLeft = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .ForeignKey("dbo.Designation", t => t.DesignationId)
                .Index(t => t.DesignationId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Designation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DesignationName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EnrollCourse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNo = c.String(nullable: false),
                        CourseId = c.Int(nullable: false),
                        EnrollDate = c.DateTime(nullable: false),
                        CourseGrade = c.String(),
                        Student_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId)
                .ForeignKey("dbo.Student", t => t.Student_Id)
                .Index(t => t.CourseId)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        ContactNo = c.String(nullable: false, maxLength: 14),
                        Date = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        StudentRegNo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Grade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GradeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EnrollCourse", "Student_Id", "dbo.Student");
            DropForeignKey("dbo.Student", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.EnrollCourse", "CourseId", "dbo.Course");
            DropForeignKey("dbo.CourseAssign", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.Teacher", "DesignationId", "dbo.Designation");
            DropForeignKey("dbo.Teacher", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.CourseAssign", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.CourseAssign", "CourseID", "dbo.Course");
            DropForeignKey("dbo.ClassRoomAllocation", "RoomId", "dbo.Room");
            DropForeignKey("dbo.ClassRoomAllocation", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.ClassRoomAllocation", "DayId", "dbo.Day");
            DropForeignKey("dbo.ClassRoomAllocation", "CourseId", "dbo.Course");
            DropForeignKey("dbo.Course", "SemesterId", "dbo.Semester");
            DropForeignKey("dbo.Course", "DepartmentId", "dbo.Department");
            DropIndex("dbo.Student", new[] { "DepartmentId" });
            DropIndex("dbo.EnrollCourse", new[] { "Student_Id" });
            DropIndex("dbo.EnrollCourse", new[] { "CourseId" });
            DropIndex("dbo.Teacher", new[] { "DepartmentId" });
            DropIndex("dbo.Teacher", new[] { "DesignationId" });
            DropIndex("dbo.CourseAssign", new[] { "CourseID" });
            DropIndex("dbo.CourseAssign", new[] { "TeacherId" });
            DropIndex("dbo.CourseAssign", new[] { "DepartmentId" });
            DropIndex("dbo.Course", new[] { "SemesterId" });
            DropIndex("dbo.Course", new[] { "DepartmentId" });
            DropIndex("dbo.ClassRoomAllocation", new[] { "DayId" });
            DropIndex("dbo.ClassRoomAllocation", new[] { "RoomId" });
            DropIndex("dbo.ClassRoomAllocation", new[] { "CourseId" });
            DropIndex("dbo.ClassRoomAllocation", new[] { "DepartmentId" });
            DropTable("dbo.Grade");
            DropTable("dbo.Student");
            DropTable("dbo.EnrollCourse");
            DropTable("dbo.Designation");
            DropTable("dbo.Teacher");
            DropTable("dbo.CourseAssign");
            DropTable("dbo.ClassSchedule");
            DropTable("dbo.Room");
            DropTable("dbo.Day");
            DropTable("dbo.Semester");
            DropTable("dbo.Department");
            DropTable("dbo.Course");
            DropTable("dbo.ClassRoomAllocation");
        }
    }
}
