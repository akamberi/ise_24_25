using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ILessonFileService
    {
        Task<IEnumerable<LessonFileDTO>> GetAllAsync();
        Task<LessonFileDTO> GetByIdAsync(int id);
        Task<LessonFileDTO> UploadAsync(CreateLessonFileDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
