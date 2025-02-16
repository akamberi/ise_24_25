using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using Common.DTOs;
using DAL.Data;
using DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;


namespace BLL.Services
{
        public class AssignmentService : IAssignmentService
        {
            private readonly ApplicationDbContext _context;

            public AssignmentService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Assignment> CreateAssignmentAsync(CreateAssignmentDto dto)
            {
                var assignment = new Assignment
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    GoogleFormUrl = dto.GoogleFormUrl,
                    CourseId = dto.CourseId,
                    DueDate = dto.DueDate
                };

                _context.Assignments.Add(assignment);
                await _context.SaveChangesAsync();
                return assignment;
            }

            public async Task<Assignment> GetAssignmentByIdAsync(int id)
            {
                return await _context.Assignments
                    .Include(a => a.Course)  // Load related course if needed
                    .FirstOrDefaultAsync(a => a.Id == id);
            }

            public async Task<IEnumerable<Assignment>> GetAllAssignmentsAsync()
            {
                return await _context.Assignments
                    .Include(a => a.Course)
                    .ToListAsync();
            }

            public async Task UpdateAssignmentAsync(int id, CreateAssignmentDto dto)
            {
                var assignment = await _context.Assignments.FindAsync(id);
                if (assignment != null)
                {
                    assignment.Title = dto.Title;
                    assignment.Description = dto.Description;
                    assignment.GoogleFormUrl = dto.GoogleFormUrl;
                    assignment.CourseId = dto.CourseId;
                    assignment.DueDate = dto.DueDate;

                    await _context.SaveChangesAsync();
                }
            }

            public async Task DeleteAssignmentAsync(int id)
            {
                var assignment = await _context.Assignments.FindAsync(id);
                if (assignment != null)
                {
                    _context.Assignments.Remove(assignment);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }



