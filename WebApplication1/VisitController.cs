using Api.Abstractions;
using Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/visits")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;

        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        [HttpPost]
        public IActionResult AddVisist([FromBody] CreateVisitDto dto)
        {
            try
            {
                return Ok(_visitService.AddNewVisit(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}