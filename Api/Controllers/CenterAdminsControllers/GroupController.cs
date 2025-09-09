using Api.Responses;
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
    }
}
