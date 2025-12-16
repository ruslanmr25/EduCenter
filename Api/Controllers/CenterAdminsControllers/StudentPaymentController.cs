using Api.Responses;
using Application.DTOs.PaymentsDto;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers.CenterAdminsControllers;

[Route("api/[controller]")]
[ApiController]
public class StudentPaymentController : ControllerBase
{
    protected readonly PaymentRepository paymentRepository;

    public StudentPaymentController(PaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    [HttpGet("students/{studentId}")]
    public async Task<IActionResult> Info(int studentId)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);

        Student? student = await paymentRepository.GetStudentPaymentSyclesAsync(
            studentId,
            centerId
        );

        if (student is null)
        {
            return NotFound();
        }
        return Ok(new ApiResponse<Student>(student));
    }

    [HttpGet("groups/{groupId}")]
    public async Task<IActionResult> GroupInfo(int groupId)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);

        Group? group = await paymentRepository.GetGroupPaymentSycleAsync(groupId, centerId);

        if (group is null)
        {
            return NotFound();
        }

        return Ok(new ApiResponse<Group>(group));
    }

    [HttpPost("pay")]
    public async Task<IActionResult> Pay(StudentPaymentDto dto)
    {
        var studentPayment = new StudentPayment
        {
            BeginDate = dto.BeginDate,
            EndDate = dto.EndDate,
            GroupStudentPaymentSycleId = dto.GroupStudentPaymentSycleId,
            Amount = dto.Amount,
        };

        await paymentRepository.PayAsync(studentPayment);
        return Ok(new ApiResponse<object>());
    }

    [HttpGet("pending-fees")]
    public async Task<IActionResult> GetPendingFeeStudents()
    {
        List<GroupStudentPaymentSycle> students = await paymentRepository.PendingFees();

        return Ok(new ApiResponse<List<GroupStudentPaymentSycle>>(students));
    }
}
