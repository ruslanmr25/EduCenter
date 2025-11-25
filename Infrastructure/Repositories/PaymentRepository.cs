using System;
using System.ComponentModel;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentRepository(AppDbContext appDbContext)
{
    protected AppDbContext _context = appDbContext;

    public async Task CreateGroupStudentPaymentAsync(Group group, Student student)
    {
        if (
            group is null
            || (
                await _context
                    .GroupStudentPaymentSycles.Where(gs =>
                        gs.GroupId == group.Id && gs.StudentId == student.Id
                    )
                    .FirstOrDefaultAsync()
                is not null
            )
        )
        {
            return;
        }

        GroupStudentPaymentSycle paymentSycle = new GroupStudentPaymentSycle
        {
            StudentId = student.Id,
            GroupId = group.Id,
            Price = group.GroupPrice,
        };

        await _context.GroupStudentPaymentSycles.AddAsync(paymentSycle);
        await _context.SaveChangesAsync();
    }

    public async Task CreateGroupStudentPaymentAsync(List<Group> groups, Student student)
    {
        var groupIds = groups.Select(g => g.Id).ToList();

        var sycles = await _context.Groups.Where(g => groupIds.Contains(g.Id)).ToListAsync();

        foreach (var sycle in sycles)
        {
            bool exists = await _context.GroupStudentPaymentSycles.AnyAsync(g =>
                g.GroupId == sycle.Id && g.StudentId == student.Id
            );

            if (exists)
                continue;

            GroupStudentPaymentSycle paymentSycle = new GroupStudentPaymentSycle
            {
                StudentId = student.Id,
                GroupId = sycle.Id,
                Price = sycle.GroupPrice,
            };

            await _context.GroupStudentPaymentSycles.AddAsync(paymentSycle);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<bool> PayAsync(List<StudentPayment> payments)
    {
        // UTC formatga o'tkazish
        foreach (var payment in payments)
        {
            payment.BeginDate = DateTime.SpecifyKind(payment.BeginDate, DateTimeKind.Utc);
            payment.EndDate = DateTime.SpecifyKind(payment.EndDate, DateTimeKind.Utc);
        }

        var newPayments = payments.Where(p => p.Id == 0).ToList();
        var existingPayments = payments.Where(p => p.Id != 0).ToList();

        if (newPayments.Count != 0)
            await _context.StudentPayments.AddRangeAsync(newPayments);

        if (existingPayments.Count != 0)
            _context.StudentPayments.UpdateRange(existingPayments);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Student?> GetStudentPaymentSyclesAsync(int studentId)
    {
        var student = await _context
            .Students.Where(s => s.Id == studentId)
            .Include(s => s.GroupStudentPaymentSycles)
            .ThenInclude(gs => gs.StudentPayments.OrderByDescending(sp => sp.CreatedAt).Take(1))
            .Include(s => s.GroupStudentPaymentSycles)
            .ThenInclude(g => g.Group)
            .FirstOrDefaultAsync();

        return student;
    }

    public async Task<Group?> GetGroupPaymentSycleAsync(int groupId)
    {
        var group = await _context
            .Groups.Where(g => g.Id == groupId)
            .Include(gs => gs.GroupStudentPaymentSycles)
            .ThenInclude(s => s.Student)
            .Include(gs => gs.GroupStudentPaymentSycles)
            .ThenInclude(g => g.StudentPayments)
            .FirstOrDefaultAsync();

        return group;
    }

    public async Task<List<Student>> PendingFeesStudents()
    {
        var today = DateTime.UtcNow.Date;

        var students = await _context
            .Students.Where(s =>
                s.GroupStudentPaymentSycles.Any(gs =>
                    gs.StudentPayments.Count == 0
                    || gs.StudentPayments.Max(p => (DateTime?)p.EndDate) <= today.AddDays(5)
                )
            )
            .Select(s => new Student
            {
                Id = s.Id,
                FullName = s.FullName,
                PhoneNumber = s.PhoneNumber,
                SecondPhoneNumber = s.SecondPhoneNumber, // agar bor bo‘lsa shu bo‘lsin
                GroupStudentPaymentSycles = s
                    .GroupStudentPaymentSycles.Select(gs => new GroupStudentPaymentSycle
                    {
                        Id = gs.Id,
                        CreatedAt = gs.CreatedAt,
                        GroupId = gs.GroupId,
                        IsActive = gs.IsActive,
                        StudentId = gs.StudentId,
                        Price = gs.Price,
                        Group = new Group { Name = gs.Group.Name },
                        StudentPayments = gs
                            .StudentPayments.OrderByDescending(sp => sp.EndDate)
                            .Take(1) // faqat eng oxirgisi
                            .Select(sp => new StudentPayment
                            {
                                Id = sp.Id,
                                EndDate = sp.EndDate,
                                Amount = sp.Amount,
                                CreatedAt = sp.CreatedAt,
                            })
                            .ToList(),
                    })
                    .ToList(),
            })
            .ToListAsync();

        return students;
    }
}
