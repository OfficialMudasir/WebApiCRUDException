using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCRUDException.Models
{
    public interface IStudentRepository
    {
        public ActionResult<IEnumerable<Student>> Get();
        void Post(Student student);
        void Update(Student student);
        void Delete(int? id);
    }
}
