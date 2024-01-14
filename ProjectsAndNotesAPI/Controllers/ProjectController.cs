using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectsAndNotesAPI.Data.Identity;
using ProjectsAndNotesAPI.Models;
using ProjectsAndNotesAPI.Repositories.ProjectRepository;

namespace ProjectsAndNotesAPI.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [Authorize]
        [RequiresRole(IdentityData.ManagerRole)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Project>> AddNewProjectAsync([FromBody] Project project)
        {
            if (project == null)
            {
                return BadRequest();
            }
            
            try
            {

                var createdProject = await _projectRepository.AddProjectAsync(project);

                return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new project");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjectsAsync()
        {
            var allProjects = await _projectRepository.GetAllProjectsAsync();
            return Ok(allProjects);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            return Ok(project);
        }

        [Authorize]
        [RequiresRole(IdentityData.AdminRole)]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Project>> UpdateProjectAsync(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            await _projectRepository.UpdateProjectAsync(project);
            return Ok(project);
        }

        [Authorize]
        [RequiresRole(IdentityData.AdminRole)]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Project>> DeleteProjectAsync(int id)
        {
            await _projectRepository.DeleteProjectAsync(id);
            return Ok();
        }
    }
}
