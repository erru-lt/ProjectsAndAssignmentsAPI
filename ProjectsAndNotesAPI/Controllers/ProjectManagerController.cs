using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectsAndNotesAPI.Data.Identity;
using ProjectsAndNotesAPI.Models;
using ProjectsAndNotesAPI.Models.DTO.ProjectManager;
using ProjectsAndNotesAPI.Repositories.ProjectManagerRepository;

namespace ProjectsAndNotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectManagerController : ControllerBase
    {
        private readonly IProjectManagerRepository _projectManagerRepository;
        private readonly IMapper _mapper;

        public ProjectManagerController(IProjectManagerRepository projectManagerRepository, IMapper mapper)
        {
            _projectManagerRepository = projectManagerRepository;
            _mapper = mapper;
        }

        [Authorize]
        [RequiresRole(IdentityData.AdminRole)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProjectManager>> AddNewProjectManagerAsync([FromBody] ProjectManager projectManager)
        {
            if (projectManager == null)
            {
                return BadRequest();
            }

            try
            {
                var createdProjectManager = await _projectManagerRepository.AddProjectManagerAsync(projectManager);
                return CreatedAtAction(nameof(GetProjectManagerById), new { id = createdProjectManager.Id }, createdProjectManager);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new project manager");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProjectManagerDto>>> GetAllProjectManagersAsync()
        {
            var projectManagers = await _projectManagerRepository.GetAllProjectManagersAsync();
            var projectManagersDto = projectManagers.Select(pm => _mapper.Map<ProjectManagerDto>(pm));
            return Ok(projectManagersDto);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProjectManagerDto>> GetProjectManagerById(int id)
        {
            var projectManager = await _projectManagerRepository.GetProjectManagerByIdAsync(id);
            var projectManagerDto = _mapper.Map<ProjectManagerDto>(projectManager);
            return Ok(projectManagerDto);
        }

        [Authorize]
        [RequiresRole(IdentityData.AdminRole)]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProjectManager>> UpdateProjectManagerAsync(int id, ProjectManager projectManager)
        {
            if(id != projectManager.Id)
            {
                return BadRequest();
            }

            await _projectManagerRepository.UpdateProjectManagerAsync(id, projectManager);
            return Ok(projectManager);
        }

        [Authorize]
        [RequiresRole(IdentityData.AdminRole)]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteProjectManagerAsync(int id)
        {
            await _projectManagerRepository.DeleteProjectManagerAsync(id);
            return Ok();
        }
    }
}
