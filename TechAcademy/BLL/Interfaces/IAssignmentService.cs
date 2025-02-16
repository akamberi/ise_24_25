using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTOs;
using DAL.Persistence.Entities;

namespace BLL.Interfaces
{
    public interface IAssignmentService
    {   Task<Assignment> CreateAssignmentAsync(CreateAssignmentDto dto);
        Task<Assignment> GetAssignmentByIdAsync(int id);
        Task<IEnumerable<Assignment>> GetAllAssignmentsAsync();
        Task UpdateAssignmentAsync(int id, CreateAssignmentDto dto);
        Task DeleteAssignmentAsync(int id);
    }
}
