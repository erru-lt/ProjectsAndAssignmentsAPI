using AutoMapper;
using ProjectsAndNotesAPI.Models;
using ProjectsAndNotesAPI.Models.DTO.Assignment;
using ProjectsAndNotesAPI.Models.DTO.ProjectManager;

namespace ProjectsAndNotesAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Assignment, AssignmentDto>();
            CreateMap<Assignment, AssignmentDtoWithoutProjectId>();
            CreateMap<ProjectManager, ProjectManagerDto>();
        }
    }
}
