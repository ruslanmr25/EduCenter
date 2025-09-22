using Api.Responses;
using Application.DTOs.GroupDTOs;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.CenterAdminsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        protected readonly GroupRepository groupRepository;
        protected readonly IMapper mapper;

        public GroupController(GroupRepository groupRepository, IMapper mapper)
        {
            this.groupRepository = groupRepository;
            this.mapper = mapper;
        }

#warning center Idni oladi
        protected int centerId = 1;

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 50)
        {
            var groups = await groupRepository.GetAllAsync(centerId: centerId, page, pageSize);
            return Ok(new ApiResponse<PagedResult<Group>>(groups));
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewGroupDto dto)
        {
            Group group = mapper.Map<Group>(dto);

            await groupRepository.CreateAsync(group);

            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            Group? group = await groupRepository.GetAsync(id);

            if (group is null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatedGroupDto dto)
        {
            Group? dbGroup = await groupRepository.GetAsync(id);

            if (dbGroup is null)
            {
                return NotFound();
            }

            mapper.Map(dto, dbGroup);

            await groupRepository.UpdateAsync(dbGroup);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await groupRepository.GetAsync(id);
            if (entity is null)
            {
                return NotFound(
                    new ApiResponse<string[]>([], success: false, message: "Hech narsa topilmadi")
                );
            }

            await groupRepository.DeleteAsync(entity);
            return Ok(new ApiResponse<string[]>());
        }
    }
}
