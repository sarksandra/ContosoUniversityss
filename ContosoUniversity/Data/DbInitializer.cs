﻿using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public class DbInitializer
    {
        public static void Initialize(SchoolContext context) 
        { 
            
            context.Database.EnsureCreated();

            
            if (context.Students.Any())
            {
                return;
            }

            
            var students = new Student[] 
            {
                new Student{FirstMidName="Nipi",LastName="Tiri",EnrollmentDate=DateTime.Parse("2069-04-20")},
                new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Oliivia",LastName="Sambery",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Marleen",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Justin",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Tiina",LastName="Meri",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{FirstMidName="Laura",LastName="Sam",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Sarah",LastName="Li",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstMidName="Saara",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstMidName="Sandra",LastName="Sark",EnrollmentDate=DateTime.Parse("2021-09-01")},
            };

            
            foreach (Student student in students)
            {
                context.Students.Add(student);
            }
            
            context.SaveChanges();

            
            var courses = new Course[]
            {
                new Course{CourseID=1050,Title="Keemia",Credits=3},
                new Course{CourseID=4022,Title="Matemaatika",Credits=3},
                new Course{CourseID=4041,Title="Loodusõpetus",Credits=3},
                new Course{CourseID=1045,Title="Calculus",Credits=4},
                new Course{CourseID=3141,Title="Trigonomeetria",Credits=4},
                new Course{CourseID=2021,Title="Kompositsioon",Credits=3},
                new Course{CourseID=2042,Title="Kirjandus",Credits=4},
                new Course{CourseID=9001,Title="Arvutimängude Ajalugu",Credits=1}
            };
            foreach(Course course in courses)
            {
                context.Courses.Add(course);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
                new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
                new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},

                new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
                new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},

                new Enrollment{StudentID=3,CourseID=1050},

                new Enrollment{StudentID=4,CourseID=1050},
                new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},

                new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},

                new Enrollment{StudentID=6,CourseID=1045},

                new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},

                new Enrollment{StudentID=10,CourseID=9001,Grade=Grade.A},
            };
            foreach (Enrollment enrollment in enrollments)
            {
                context.Enrollments.Add(enrollment);
            }
            context.SaveChanges();
            if (context.Instructors.Any())
            {
                return;
            }
            var instructors = new Instructor[]
            {
                new Instructor{FirstMidName = "Ying", LastName = "Yang",  HireDate = DateTime.Parse("2019-09-01")},
                new Instructor{FirstMidName = "Original", LastName = "Name", HireDate = DateTime.Parse("1776-09-01")},
                new Instructor{FirstMidName = "This", LastName = "One",  HireDate = DateTime.Parse("1776-09-01")},
            };
            context.Instructors.AddRange(instructors);
            context.SaveChanges();


            if (context.Departments.Any())
            {
                return;
            }
            var departments = new Department[]
            {
                new Department{
                    Name="IT",
                    Budget=0,
                    StartDate=DateTime.Parse("3024-06-03"),
                    InstructorID=1,
                    DepartmentDog="Kiisu",
                },
                new Department{
                    Name="Language",
                    Budget=0,
                    StartDate=DateTime.Parse("2024-01-12"),
                    InstructorID=1,
                    DepartmentDog="Miisu",
                },
                new Department{
                    Name="Everything else",
                    Budget=0,
                    StartDate=DateTime.Parse("3011-08-04"),
                    DepartmentDog="Tanno",
                },
            };
            context.Departments.AddRange(departments);
            context.SaveChanges();
           

        }

    }
}
