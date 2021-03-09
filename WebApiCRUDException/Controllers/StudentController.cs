using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCRUDException.Models;

namespace WebApiCRUDException.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;

        }

        
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetValues()
        
        {
            var stud = _studentRepository.Get();
            return Ok(stud);

        }

        [HttpPost]
       public async Task<ActionResult> Post([FromBody] Student student)
        {
            if(student == null)
            {
                return NotFound();
            }
            try
            {
                _studentRepository.Post(student);
                return Ok("Value Added");

            }
            catch(Exception e)
            {
                return BadRequest(e);

            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Student student)
        {
            if(student == null)
            {
                return NotFound("Student is null, cannot update");
            }
            try
            {
                _studentRepository.Update(student);
                return Ok("Value Updated");
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound("Geting Null For Student Id");

            }
            try
            {
               _studentRepository.Delete(id);
                return Ok("Value Deleted");
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
