using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<ActionResult<Project>> AddNewProjectAsync([FromBody] Project project)
        {
            try
            {
                if (project == null)
                {
                    return BadRequest();
                }

                var createdProject = await _projectRepository.AddProjectAsync(project);

                return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new project");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjectsAsync()
        {
            var allProjects = await _projectRepository.GetAllProjectsAsync();
            return Ok(allProjects);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            return Ok(project);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Project>> UpdateProjectAsync(int id, Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            await _projectRepository.UpdateProjectAsync(project);
            return Ok(project);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Project>> DeleteProjectAsync(int id)
        {
            await _projectRepository.DeleteProjectAsync(id);
            return Ok();
        }
    }
}
