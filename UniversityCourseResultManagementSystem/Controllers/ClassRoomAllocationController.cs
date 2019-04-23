using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementSystem;
using UniversityCourseResultManagementSystem.Models;

namespace UniversityCourseResultManagementSystem.Controllers
{
    public class ClassRoomAllocationController : Controller
    {
        private UniversityCourseResultManagementSystemDbContext db = new UniversityCourseResultManagementSystemDbContext();

        // GET: /ClassRoomAllocation/Index
        public ActionResult Index()
        {
            // var classroomallocations = db.ClassRoomAllocations.Include(c => c.Course).Include(c => c.Day).Include(c => c.Department).Include(c => c.Room);         
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode");
            return View();
        }

        // GET: /ClassRoomAllocation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            if (classroomallocation == null)
            {
                return HttpNotFound();
            }
            return View(classroomallocation);
        }

        // GET: /ClassRoomAllocation/Create
        public ActionResult Create()
        {
            //ViewBag.CourseId = new SelectList(db.Courses.Where(s => s.CourseStatus == true).ToList());
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode");
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name");
            //ViewBag.courses = db.Courses.Where(s => s.CourseStatus == true).ToList();

            return View();
        }

        // POST: /ClassRoomAllocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepartmentId,CourseId,RoomId,DayId,StartTime,EndTime,RoomStatus")] ClassRoomAllocation classroomallocation)
        {
            if (ModelState.IsValid)
            {
                db.ClassRoomAllocations.Add(classroomallocation);
                db.SaveChanges();
            }

            //ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", classroomallocation.CourseId);
            //ViewBag.DayId = new SelectList(db.Days, "Id", "Name", classroomallocation.DayId);
            //ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", classroomallocation.DepartmentId);
            //ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", classroomallocation.RoomId);
            return View(classroomallocation);
        }
        public JsonResult GetCoursesByDeptId(int deptId)
        {
            var courses = db.Courses.Where(m => m.DepartmentId == deptId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveRoomSchedule(ClassRoomAllocation classRoomAllocation)
        {
            var scheduleList = db.ClassRoomAllocations.Where(m => m.RoomId == classRoomAllocation.RoomId && m.DayId == classRoomAllocation.DayId && m.RoomStatus == "Allocated").ToList();
            if (scheduleList.Count == 0)
            {
                classRoomAllocation.RoomStatus = "Allocated";
                db.ClassRoomAllocations.Add(classRoomAllocation);
                db.SaveChanges();
                return Json(true);
            }
            else
            {
                bool status = false;
                foreach (var allocation in scheduleList)
                {
                    if ((classRoomAllocation.StartTime >= allocation.StartTime && classRoomAllocation.StartTime < allocation.EndTime)
                         || (classRoomAllocation.EndTime > allocation.StartTime && classRoomAllocation.EndTime <= allocation.EndTime) && classRoomAllocation.RoomStatus == "Allocated")
                    {
                        status = true;
                    }
                }
                if (status == false)
                {
                    classRoomAllocation.RoomStatus = "Allocated";
                    db.ClassRoomAllocations.Add(classRoomAllocation);
                    db.SaveChanges();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }

        }

        // GET: /ClassRoomAllocation/ViewClassSchedule
        public ActionResult ViewClassSchedule()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode");
            return View();
        }
        ////public JsonResult ShowClassScheduleInfo(int deptId)
        ////{
        ////    var classSchedule = db.ClassRoomAllocations.Where(m => m.DepartmentId == deptId).ToList();
        ////    return Json(classSchedule, JsonRequestBehavior.AllowGet);
        ////}
        public JsonResult GetClassScheduleInfo(int deptId)
        {
            var courses = db.Courses.Where(t => t.DepartmentId == deptId).ToList();
            List<ClassSchedule> classSchedules = new List<ClassSchedule>();
            foreach (var course in courses)
            {
                string schedule = "";
                int counter = 0;
                var courseSchedules = db.ClassRoomAllocations.Where(t => t.DepartmentId == course.DepartmentId && t.CourseId == course.Id && t.RoomStatus == "Allocated").ToList();
                foreach (var courseSchedule in courseSchedules)
                {
                    if (counter != 0)
                    {
                        schedule += "; ||";
                    }
                    schedule += "Room No. : " + courseSchedule.Room.Name + ", " + courseSchedule.Day.Name.Substring(0, 3) + ", ";// Why Name.Substring(0, 3)???
                    int hour, minute;
                    string part = "AM";
                    int startTime = (int)courseSchedule.StartTime - 1;
                    hour = startTime / 60;
                    minute = startTime - (hour * 60);
                    if (startTime >= 720)
                    {
                        hour -= 12; //hour =hour- 12;
                        if (hour == 0)
                        {
                            hour = 12;
                        }
                        part = "PM";
                    }
                    schedule += hour + ":" + minute.ToString("00") + " " + part + " - ";
                    int endTime = (int)courseSchedule.EndTime;
                    hour = endTime / 60;
                    minute = endTime - (hour * 60);
                    part = "AM";
                    if (endTime >= 720)
                    {
                        hour -= 12;
                        if (hour == 0)
                        {
                            hour = 12;
                        }
                        part = "PM";
                    }
                    schedule += hour + ":" + minute.ToString("00") + " " + part;
                    counter++;
                }

                if (schedule == "")
                {
                    schedule = "Not Scheduled Yet.";
                }
                ClassSchedule classSchedule = new ClassSchedule(course.CourseCode, course.CourseName, schedule);
                classSchedules.Add(classSchedule);
            }
            return Json(classSchedules, JsonRequestBehavior.AllowGet);
        }

        //Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            if (classroomallocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", classroomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", classroomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", classroomallocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", classroomallocation.RoomId);
            return View(classroomallocation);
        }

        // POST: /ClassRoomAllocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartmentId,CourseId,RoomId,DayId,StartTime,EndTime,RoomStatus")] ClassRoomAllocation classroomallocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classroomallocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", classroomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", classroomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", classroomallocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", classroomallocation.RoomId);
            return View(classroomallocation);
        }

        // GET: /ClassRoomAllocation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            if (classroomallocation == null)
            {
                return HttpNotFound();
            }
            return View(classroomallocation);
        }

        // POST: /ClassRoomAllocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            db.ClassRoomAllocations.Remove(classroomallocation);
            db.SaveChanges();
            return RedirectToAction("");
        }

        public ActionResult UnallocatedClassRoom()
        {
            return View();
        }

        public JsonResult UnAllocateAllRooms(bool decision)
        {
            var roomStatus = db.ClassRoomAllocations.Where(m => m.RoomStatus == "Allocated").ToList();
            if (roomStatus.Count == 0)
            {
                return Json(false);
            }
            else
            {
                foreach (var room in roomStatus)
                {
                    room.RoomStatus = "";
                    db.ClassRoomAllocations.AddOrUpdate(room);
                    db.SaveChanges();

                }
                return Json(true);
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
