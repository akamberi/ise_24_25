using Aspose.Slides;
using BLL.Interfaces;
using Common.DTOs;
using DAL.Data;
using DAL.Persistence.Entities;
using DAL.Persistence.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LessonFileService : ILessonFileService
    {
        private readonly ILessonFileRepository _lessonFileRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public LessonFileService(ILessonFileRepository lessonFileRepository, IWebHostEnvironment environment, ApplicationDbContext context)
        {
            _lessonFileRepository = lessonFileRepository;
            _environment = environment;
            _context = context;
        }

        // ✅ Get all lesson files
        public async Task<IEnumerable<LessonFileDTO>> GetAllAsync()
        {
            var files = await _lessonFileRepository.GetAllAsync();

            // Include CourseModule name by joining with CourseModule table
            return files.Select(f => new LessonFileDTO
            {
                Id = f.Id,
                CourseModuleId = f.CourseModuleId,
                CourseModuleName = _context.CourseModules
                                        .Where(c => c.Id == f.CourseModuleId)
                                        .Select(c => c.Title)
                                        .FirstOrDefault(),  // Fetch Course Module Name
                FileName = f.FileName,
                FilePath = f.FilePath,
                FileType = f.FileType,
                FileSize = f.FileSize
            });
        }

        // ✅ Get lesson file by ID
        public async Task<LessonFileDTO> GetByIdAsync(int id)
        {
            var file = await _lessonFileRepository.GetByIdAsync(id);
            if (file == null) return null;

            // Include CourseModule name by joining with CourseModule table
            return new LessonFileDTO
            {
                Id = file.Id,
                CourseModuleId = file.CourseModuleId,
                CourseModuleName = _context.CourseModules
                                            .Where(c => c.Id == file.CourseModuleId)
                                            .Select(c => c.Title)
                                            .FirstOrDefault(), // Fetch Course Module Name
                FileName = file.FileName,
                FilePath = file.FilePath,
                FileType = file.FileType,
                FileSize = file.FileSize
            };
        }

        // ✅ Upload lesson file
        public async Task<LessonFileDTO> UploadAsync(CreateLessonFileDTO dto)
        {
            if (dto.File == null || dto.File.Length == 0)
            {
                throw new InvalidOperationException("No file provided");
            }

            // Generate a unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.File.FileName);

            // Define your custom upload directory (outside wwwroot)
            var uploadDirectory = @"C:\Users\User\OneDrive\Desktop\Uploads";

            // Ensure the directory exists
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            // Full file path
            var filePath = Path.Combine(uploadDirectory, fileName);

            // Save the file to the specified directory
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            // Create the LessonFile object
            var lessonFile = new LessonFile
            {
                CourseModuleId = dto.CourseModuleId,
                FileName = fileName,
                FileType = dto.File.ContentType,
                FileSize = dto.File.Length,
                FilePath = filePath // Save the absolute path in the database
            };

            // Save to the database
            await _context.LessonFiles.AddAsync(lessonFile);
            await _context.SaveChangesAsync();

            // Return a LessonFileDTO instead of LessonFile
            return new LessonFileDTO
            {
                Id = lessonFile.Id,
                CourseModuleId = lessonFile.CourseModuleId,
                FileName = lessonFile.FileName,
                FilePath = lessonFile.FilePath,
                FileType = lessonFile.FileType,
                FileSize = lessonFile.FileSize
            };
        }


        // ✅ Delete lesson file
        public async Task<bool> DeleteAsync(int id)
        {
            var file = await _lessonFileRepository.GetByIdAsync(id);
            if (file == null)
                return false;

            // Get the file path from the database
            var filePath = file.FilePath;

            // Delete file from the directory
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Delete record from the database
            return await _lessonFileRepository.DeleteAsync(id);
        }

        public async Task<byte[]> GetFileContentAsync(int id)
        {
            var file = await _lessonFileRepository.GetByIdAsync(id);
            if (file == null) return null;

            if (!File.Exists(file.FilePath))
            {
                throw new FileNotFoundException("File not found on server.");
            }

            return await File.ReadAllBytesAsync(file.FilePath);
        }

        public async Task<byte[]> ConvertPptxToPdfAsync(int id)
        {
            var file = await _lessonFileRepository.GetByIdAsync(id);
            if (file == null ||
               !(file.FileType == "application/vnd.ms-powerpoint" ||
                 file.FileType == "application/vnd.openxmlformats-officedocument.presentationml.presentation"))
            {
                return null; // Not a PowerPoint file
            }

            var pdfPath = Path.Combine(Path.GetDirectoryName(file.FilePath),
                                        Path.GetFileNameWithoutExtension(file.FileName) + ".pdf");

            using (Presentation pres = new Presentation(file.FilePath))
            {
                pres.Save(pdfPath, Aspose.Slides.Export.SaveFormat.Pdf);
            }

            return await File.ReadAllBytesAsync(pdfPath); // Return converted PDF file
        }



    }

}
