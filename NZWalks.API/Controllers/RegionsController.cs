using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using System.Text.RegularExpressions;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GetAll Regions
        // GET: api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get data from database - domain models
            var regionsDomain = dbContext.Regions.ToList();

            // Map to DTOs - Data Transfer Objects
            var regionDto = new List<RegionDto>();

            foreach(var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDto()
                {
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            return Ok(regionDto);
        }

        // Get Region by Id
        // GET: api/regions/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = dbContext.Regions.Find(id);

            if(region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }
    }
}
