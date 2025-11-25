using System;
using Application.DTOs.StatisticDto;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

// using Application.DTOs.
namespace Infrastructure.Repositories;

public class CenterStatisticRepository
{
    protected readonly AppDbContext dbContext;

    public CenterStatisticRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<CenterStatisticDto> GetStatistic(Center center)
    {
        int studentCount = await dbContext
            .GroupStudentPaymentSycles.Where(g => g.IsActive)
            .Select(g => g.StudentId)
            .Distinct()
            .CountAsync();

        int groupCount = await dbContext.Groups.CountAsync(g => g.IsActive);

        int teacherCount = await dbContext
            .Users.Where(u => u.Role == Role.Teacher && u.Centers.Contains(center))
            .CountAsync();

        int scienceCount = await dbContext.Sciences.CountAsync(s => s.CenterId == center.Id);

        CenterStatisticDto dto = new()
        {
            ActiveStudentCount = studentCount,
            ActiveGroupCount = groupCount,
            TeacherCount = teacherCount,
            ScienceCount = scienceCount,
        };

        return dto;
    }
}
