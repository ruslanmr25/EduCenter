using Api.Responses;
using Application.DTOs.PaymentsDto;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        Student? student = await paymentRepository.GetStudentPaymentSyclesAsync(studentId);
        return Ok(new ApiResponse<Student>(student));
    }

    [HttpGet("groups/{groupId}")]
    public async Task<IActionResult> GroupInfo(int groupId)
    {
        Group? group = await paymentRepository.GetGroupPaymentSycleAsync(groupId);

        return Ok(new ApiResponse<Group>(group));
    }

    [HttpPost("students/{studentId}/pay")]
    public async Task<IActionResult> Pay(int studentId, [FromBody] List<StudentPaymentDto> dto)
    {
        if (dto == null || dto.Count == 0)
            return BadRequest("To‘lov ma’lumotlari kiritilmagan.");

        var studentPayments = dto.Select(d => new StudentPayment
            {
                BeginDate = d.BeginDate.ToDateTime(TimeOnly.MaxValue),
                EndDate = d.EndDate.ToDateTime(TimeOnly.MaxValue),
                GroupStudentPaymentSycleId = d.GroupStudentPaymentSycleId,
                Amount = d.Amount,
            })
            .ToList();

        await paymentRepository.PayAsync(studentPayments);

        return Ok(new ApiResponse<object>());
    }

    [HttpGet("students/pending-fees")]
    public async Task<IActionResult> GetPendingFeeStudents()
    {
        var students = await paymentRepository.PendingFeesStudents();

        return Ok(new ApiResponse<List<Student>>(students));
    }
}
