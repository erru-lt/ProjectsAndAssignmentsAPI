using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectsAndNotesAPI.Data.Identity;
using ProjectsAndNotesAPI.Models;
using ProjectsAndNotesAPI.Models.DTO.Assignment;
using ProjectsAndNotesAPI.Repositories.AssignmentRepository;

namespace ProjectsAndNotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentRepository? _assignmentRepository;
        private readonly IMapper _mapper;

        public AssignmentController(IAssignmentRepository? assignmentRepository, IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
        }

        [Authorize]
        [RequiresRole(IdentityData.ManagerRole)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Assignment>> AddAssignmentAsync([FromBody] Assignment assignment)
        {
            if (assignment == null)
            {
                return BadRequest();
            }

            try
            {
                var createdAssignment = await _assignmentRepository.AddAssignmentAsync(assignment);
                return CreatedAtAction(nameof(GetAssignmentById), new { id = createdAssignment.Id }, createdAssignment);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new assignment");
            }
        }

        [AllowAnonymous]
        [HttpGet("byProject/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AssignmentDtoWithoutProjectId>>> GetAssignmentsByProjectAsync(int projectId)
        {
            var assignments = await _assignmentRepository.GetAssignmentsByProjectAsync(projectId);
            var assignmentsDto = assignments.Select(a => _mapper.Map<AssignmentDtoWithoutProjectId>(a));
            return Ok(assignmentsDto);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AssignmentDto>> GetAssignmentById(int id)
        {
            var assignment = await _assignmentRepository.GetAssignmentByIdAsync(id);
            var assignmentDto = _mapper.Map<AssignmentDto>(assignment);
            return Ok(assignmentDto);
        }

        [Authorize]
        [RequiresRole(IdentityData.AdminRole)]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Assignment>> UpdateAssignmentAsync(int id, Assignment assignment)
        {
            if(id != assignment.Id)
            {
                return BadRequest();
            }

            await _assignmentRepository.UpdateAssignmentAsync(assignment);

            return Ok(assignment);
        }

        [Authorize]
        [RequiresRole(IdentityData.AdminRole)]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteAssignmentAsync(int id)
        {
            await _assignmentRepository.DeleteAssignmentAsync(id);
            return Ok();
        }
    }
}
