using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Persistence.Entities;
using Common.DTOs;


namespace BLL.Interfaces
{
    public interface IAssignmentSubmissionService
    {
        Task<AssignmentSubmission> CreateAssignmentSubmissionAsync(CreateAssignmentSubmissionDto dto);
        Task<AssignmentSubmission> GetAssignmentSubmissionByIdAsync(int id);
        Task<IEnumerable<AssignmentSubmission>> GetAllAssignmentSubmissionsAsync(); 

        Task UpdateAssignmentSubmissionAsync(int id, CreateAssignmentSubmissionDto dto);
        Task DeleteAssignmentSubmissionAsync(int id);

    }
}
