using HelpUs.API.DataTransferObjects.Requests;
using HelpUs.API.Services.ProjectServices;
using Microsoft.AspNetCore.Mvc;

namespace HelpUs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Add([FromForm] ProjectCreateDto projectCreateDto, IFormFile image)
        {
          var result = await  projectService.AddProject(projectCreateDto, image);
            return Ok(result);
        }
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromForm] ProjectEditDto projectEditDto, string id, IFormFile? image)
        {
            var result = await projectService.Edit(projectEditDto,id, image!);
            return Ok(result);
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result =  projectService.GetAll();
            return Ok(result);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await projectService.GetById(id);
            return Ok(result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await projectService.Delete(id);
            return Ok(result);
        }
    }
}
