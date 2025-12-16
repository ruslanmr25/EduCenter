using Api.Responses;
using Application.DTOs.CenterDTOs;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SuperAdminsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
    public class CenterController : ControllerBase
    {
        protected CenterRepository centerRepository;

        protected IMapper mapper;

        public CenterController(CenterRepository centerRepository, IMapper mapper)
        {
            this.centerRepository = centerRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 50)
        {
            PagedResult<Center> centers = await centerRepository.GetAllAsync(page, pageSize);

            return Ok(new ApiResponse<PagedResult<Center>>(item: centers));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Show(int id)
        {
            Center? center = await centerRepository.GetAsync(id);

            if (center is null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<Center>(center));
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewCenterDTO centerDto)
        {
            Center center = mapper.Map<Center>(centerDto);

            await centerRepository.CreateAsync(center);
            // Center center = ;
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatedCenterDTO centerDTO)
        {
            Center? dbCenter = await centerRepository.GetAsync(id);
            if (dbCenter is null)
            {
                return NotFound();
            }

            mapper.Map(centerDTO, dbCenter);

            await centerRepository.UpdateAsync(dbCenter);

            return Ok(new ApiResponse<string[]>());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await centerRepository.GetAsync(id);
            if (entity is null)
            {
                return NotFound(
                    new ApiResponse<string[]>([], success: false, message: "Hech narsa topilmadi")
                );
            }

            await centerRepository.DeleteAsync(entity);
            return Ok(new ApiResponse<string[]>());
        }
    }
}
