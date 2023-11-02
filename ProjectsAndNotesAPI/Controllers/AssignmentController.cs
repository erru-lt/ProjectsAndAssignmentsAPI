using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectsAndNotesAPI.Models;
using ProjectsAndNotesAPI.Models.DTO;
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

        [HttpGet("byProject/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AssignmentDtoWithoutProjectId>>> GetAssignmentsByProjectAsync(int projectId)
        {
            var assignments = await _assignmentRepository.GetAssignmentsByProjectAsync(projectId);
            var assignmentsDto = assignments.Select(a => _mapper.Map<AssignmentDtoWithoutProjectId>(a));
            return Ok(assignmentsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AssignmentDto>> GetAssignmentById(int id)
        {
            var assignment = await _assignmentRepository.GetAssignmentByIdAsync(id);
            var assignmentDto = _mapper.Map<AssignmentDto>(assignment);
            return Ok(assignmentDto);
        }

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

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteAssignmentAsync(int id)
        {
            await _assignmentRepository.DeleteAssignmentAsync(id);
            return Ok();
        }
    }
}
