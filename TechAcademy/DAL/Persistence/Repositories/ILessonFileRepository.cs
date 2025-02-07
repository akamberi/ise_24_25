using DAL.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence.Repositories
{
    public interface ILessonFileRepository
    {
        Task<IEnumerable<LessonFile>> GetAllAsync();
        Task<LessonFile> GetByIdAsync(int id);
        Task AddAsync(LessonFile lessonFile);
        Task<bool> DeleteAsync(int id);
        Task DeleteAsync(LessonFile file);
        Task UpdateAsync(LessonFile file); // Add this method

    }
}
