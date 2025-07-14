using System;
using System.Data.Entity.Validation;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GestAca.Entities;
using GestAca.Persistence;
using System.Net;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace DBTest
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                new Program();
            }
            catch (Exception e)
            {
                printError(e);
            }
            Console.WriteLine("\nPulse una tecla para salir");
            Console.ReadLine();
        }

        static void printError(Exception e)
        {
            while (e != null)
            {
                if (e is DbEntityValidationException)
                {
                    DbEntityValidationException dbe = (DbEntityValidationException)e;

                    foreach (var eve in dbe.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                ve.PropertyName,
                                eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                ve.ErrorMessage);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }
                e = e.InnerException;
            }
        }


        Program()
        {
            IDAL dal = new EntityFrameworkDAL(new GestAcaDbContext());

            CreateSampleDB(dal);
            PrintSampleDB(dal);
        }


        private void CreateSampleDB(IDAL dal)
        {
            dal.RemoveAllData();

            Console.WriteLine("CREANDO LOS DATOS Y ALMACENANDOLOS EN LA BD");
            Console.WriteLine("===========================================");

            Console.WriteLine("\n// CREACIÓN DE CURSOS");
            //public Course(string descr, string name)
            Course aCourse1 = new Course("Curso Introductorio Ingenieria Software", "Software Engineering");
            dal.Insert<Course>(aCourse1);
            dal.Commit();
            Course aCourse2 = new Course("Curso Introductorio de Estructuras de datos", "Data Structures");
            dal.Insert<Course>(aCourse2);
            dal.Commit();

            // Populate here the rest of the database
            // Add missing code here
        }

        
        private void PrintSampleDB(IDAL dal)
        {
            Console.WriteLine("\n\nMOSTRANDO LOS DATOS DE LA BD");
            Console.WriteLine("============================\n");

            Console.WriteLine("\nCursos creados:");
            foreach (Course c in dal.GetAll<Course>())
                Console.WriteLine("   Name: " + c.Name + " Description: " + c.Description);

            // Show the rest of the database
            Console.WriteLine("\nProfesores creados:");
            foreach (Teacher t in dal.GetAll<Teacher>())
                Console.WriteLine("   ID: " + t.Id + " Name: " + t.Name);

            Console.WriteLine("\nImparticiones de cursos creados:");
            foreach(Course c in dal.GetAll<Course>())
            {
                Console.WriteLine("   Name: " + c.Name + " Description: " + c.Description);
                foreach (TaughtCourse tc in c.TaughtCourses)
                    Console.WriteLine("      ID: " + tc.Id + " START: " + tc.StartDateTime + " END: " + tc.EndDate);
            }

            Console.WriteLine("\nInscripciones por curso a impartir:");
            foreach (Course c in dal.GetAll<Course>())
            {
                Console.WriteLine("   Name: " + c.Name + " Description: " + c.Description);
                foreach (TaughtCourse tc in c.TaughtCourses)
                {
                    Console.WriteLine("      ID: " + tc.Id + " START: " + tc.StartDateTime + " END: " + tc.EndDate);
                    foreach(Enrollment en in tc.Enrollments)
                        Console.WriteLine("      ---> Student: " + en.Student.Name + " Enrollment Date: " + en.EnrollmentDate);
                }

            }

            Console.WriteLine("\nAulas creadas y sus asignaciones");
            foreach(Classroom o in dal.GetAll<Classroom>())
            {
                Console.WriteLine("   Name: " + o.Name + " Capacity: " + o.MaxCapacity);
                foreach(TaughtCourse tc in o.TaughtCourses)
                    Console.WriteLine("      CourseID: " + tc.Id + " START: " + tc.StartDateTime + " END: " + tc.EndDate + " People: " + tc.Enrollments.Count );
            }

            Console.WriteLine("\nFaltas de asistencia por alumno");
            foreach(Student s in dal.GetAll<Student>())
            {
                Console.WriteLine("   Student Name: " + s.Name);
                foreach(Enrollment en in s.Enrollments)
                {
                    Console.WriteLine("      Enrollment in: " + en.TaughtCourse.Id + " Course name: " + en.TaughtCourse.Course.Name + " Absences: " + en.Absences.Count);
                    foreach (Absence ab in en.Absences)
                        Console.WriteLine("         Date: " + ab.Date);
                }

            }
        }

    }

}
