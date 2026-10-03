using _24_NguyenManhCuong_Assignment01_BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace _24_NguyenManhCuong_Assignment01_BackEnd.Controllers
{
    [Route("odata/[controller]")]
    public class TagsController : ODataController
    {
        private readonly ITagService _service;

        public TagsController(ITagService service)
        {
            _service = service;
        }

        [HttpGet]
        [EnableQuery(PageSize = 100)]
        public IActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tag = await _service.GetByIdAsync(id);
            if (tag == null) return NotFound(new { message = $"Tag with ID {id} not found." });
            return Ok(tag);
        }
    }
}
