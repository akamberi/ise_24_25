using DAL.Data;
using DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence.Repositories
{
    public class LessonFileRepository : ILessonFileRepository
    {
        private readonly ApplicationDbContext _context;

        public LessonFileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LessonFile>> GetAllAsync()
        {
            return await _context.LessonFiles.ToListAsync();
        }

        public async Task<LessonFile> GetByIdAsync(int id)
        {
            return await _context.LessonFiles.FindAsync(id);
        }

        public async Task AddAsync(LessonFile lessonFile)
        {
            await _context.LessonFiles.AddAsync(lessonFile);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var file = await _context.LessonFiles.FindAsync(id);
            if (file == null) return false;

            _context.LessonFiles.Remove(file);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task DeleteAsync(LessonFile file)
        {
            _context.LessonFiles.Remove(file);  // Remove the file from DbContext
            await _context.SaveChangesAsync();  // Save the changes
        }
        public async Task UpdateAsync(LessonFile file)
        {
            _context.LessonFiles.Update(file);  // This will track the changes and update
            await _context.SaveChangesAsync();
        }
    }
}
