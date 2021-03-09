using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCRUDException.Context;

namespace WebApiCRUDException.Models
{
    public class MockStudentRepository : IStudentRepository
    {
        private MainContext _mainContext;
        public MockStudentRepository(MainContext mainContext)
        {
            _mainContext = mainContext;

        }
        public async void Delete(int? id)
        {
            if(id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Student student = _mainContext.Students.FirstOrDefault(std => std.StudentId == id);
            if(student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            _mainContext.Students.Remove(student);
            await _mainContext.SaveChangesAsync();
            await _mainContext.DisposeAsync();
            
        }

        public ActionResult<IEnumerable<Student>> Get()
        {
            var stud = _mainContext.Students.ToList();
            return stud;
        }

        public async void Post(Student student)
        {
            if(student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }
            await _mainContext.Students.AddAsync(student);
            await _mainContext.SaveChangesAsync();
            
        }

        public async void Update(Student student)
        {
            if(student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            Student existingStudent = _mainContext.Students.FirstOrDefault(std => std.StudentId == student.StudentId);
            if(existingStudent == null)
            {
                throw new ArgumentNullException(nameof(existingStudent));
            }
            existingStudent.StudentId = student.StudentId;
            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.City = student.City;
            existingStudent.Email = student.Email;
            existingStudent.Mobile = student.Mobile;

            _mainContext.Attach(existingStudent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _mainContext.SaveChangesAsync();
        }

    }
}
