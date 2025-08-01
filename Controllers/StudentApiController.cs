using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Crud.Models;
using MongoCrudApp.Services;

namespace MongoCrudApp.Controllers
{
    //[Authorize(Roles = "Admin")] // 👈 All actions require Admin role
    [ApiController]
    [Route("api/[controller]")]
    public class StudentApiController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentApiController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Student student)
        {
            await _studentService.CreateAsync(student);
            return CreatedAtAction(nameof(GetAll), new { id = student.Id }, student);
        }
    }
}
