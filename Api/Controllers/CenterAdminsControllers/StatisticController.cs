using Api.Responses;
using Application.DTOs.StatisticDto;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers.CenterAdminsControllers
{
    [Route("api/center-admin/statistics")]
    [ApiController]
    [Authorize(Roles = "CenterAdmin")]
    public class StatisticController : ControllerBase
    {
        protected readonly CenterStatisticRepository centerStatisticRepository;

        protected readonly CenterRepository centerRepository;

        public StatisticController(
            CenterStatisticRepository centerStatisticRepository,
            CenterRepository centerRepository
        )
        {
            this.centerStatisticRepository = centerStatisticRepository;
            this.centerRepository = centerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            var CenterId = int.Parse(User.FindFirstValue("centerId")!);

            var center = await centerRepository.GetAsync(CenterId);

            var dto = await centerStatisticRepository.GetStatistic(center!);
            return Ok(new ApiResponse<CenterStatisticDto>(dto));
        }
    }
}
