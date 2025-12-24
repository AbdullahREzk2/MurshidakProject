
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Project.BLL.DTOS;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 

    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        #region get all subjects
        [HttpGet("getAllSubjects")]
        [AllowAnonymous] 
        public async Task<ActionResult<IEnumerable<SubjectDTO>>> GetAllSubjects(CancellationToken cancellationToken)
        {
            var subjects = await _subjectService.GetAllSubjectsAsync(cancellationToken);
            return Ok(subjects);
        }
        #endregion

        #region get subject by id
        [HttpGet("getSubjectBy/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<SubjectDTO>> GetSubjectById(int id, CancellationToken cancellationToken)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id, cancellationToken);
            if (subject == null)
                return NotFound(new { Message = "Subject not found" });

            return Ok(subject);
        }
        #endregion

        #region Enroll Subject
        [HttpPost("EnrollSubject")]
        public async Task<ActionResult> EnrollSubject([FromBody] EnrollSubjectUserDTO enrollDto, CancellationToken cancellationToken)
        {
            // Validation
            if(enrollDto == null || enrollDto.SubjectId <= 0)
                return BadRequest(new { Message = "Invalid enrollment data" });

            // Get current userId from JWT
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "Invalid token" });

            // Enroll
            var result = await _subjectService.EnrollSubjectAsync(enrollDto, userId, cancellationToken);
            if (!result)
                return BadRequest(new { Message = "Enrollment failed. Check prerequisites or already enrolled." });

            return Ok(new { Message = "Enrolled successfully" });
        }
        #endregion

        #region get User Enrolled Subjects
        [HttpGet("getUserEnrolledSubjects")]
        public async Task<IActionResult> GetMyEnrolledSubjects(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var subjects = await _subjectService.GetEnrolledSubjectsAsync(userId, cancellationToken);

            return Ok(subjects);
        }
        #endregion

    }
}
