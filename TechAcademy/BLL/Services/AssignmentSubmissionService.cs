using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Data;
using DAL.Persistence.Entities;
using Common.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class AssignmentSubmissionService : IAssignmentSubmissionService
    {
        private readonly ApplicationDbContext _context;

        public AssignmentSubmissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AssignmentSubmission> CreateAssignmentSubmissionAsync(CreateAssignmentSubmissionDto dto)
        {
            var submission = new AssignmentSubmission
            {
                AssignmentId = dto.AssignmentId,
                UserId = dto.UserId,
                SubmittedDate = dto.SubmittedDate,
                IsSubmitted = dto.IsSubmitted
                // Score and PassFailStatus will be null until the submission is graded.
            };

            _context.AssignmentSubmissions.Add(submission);
            await _context.SaveChangesAsync();
            return submission;
        }

        public async Task<AssignmentSubmission> GetAssignmentSubmissionByIdAsync(int id)
        {
            return await _context.AssignmentSubmissions
                .Include(s => s.Assignment)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        // method to retrieve all submissions
        public async Task<IEnumerable<AssignmentSubmission>> GetAllAssignmentSubmissionsAsync()
        {
            return await _context.AssignmentSubmissions
                .Include(s => s.Assignment)
                .Include(s => s.User)
                .ToListAsync();
        }


        public async Task UpdateAssignmentSubmissionAsync(int id, CreateAssignmentSubmissionDto dto)
        {
            var submission = await _context.AssignmentSubmissions.FindAsync(id);
            if (submission != null)
            {
                submission.AssignmentId = dto.AssignmentId;
                submission.UserId = dto.UserId;
                submission.SubmittedDate = dto.SubmittedDate;
                submission.IsSubmitted = dto.IsSubmitted;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAssignmentSubmissionAsync(int id)
        {
            var submission = await _context.AssignmentSubmissions.FindAsync(id);
            if (submission != null)
            {
                _context.AssignmentSubmissions.Remove(submission);
                await _context.SaveChangesAsync();
            }
        }

       
    }
}
