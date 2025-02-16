using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class LessonFileDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive number.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Course Module ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Course Module ID must be a positive number.")]
        public int CourseModuleId { get; set; }

        [Required(ErrorMessage = "Course Module Name is required.")]
        [MaxLength(200, ErrorMessage = "Course Module Name cannot exceed 200 characters.")]
        public string CourseModuleName { get; set; }

        [Required(ErrorMessage = "File Name is required.")]
        [MaxLength(255, ErrorMessage = "File Name cannot exceed 255 characters.")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "File Path is required.")]
        [MaxLength(1000, ErrorMessage = "File Path cannot exceed 1000 characters.")]
        public string FilePath { get; set; }

        [Required(ErrorMessage = "File Type is required.")]
        [MaxLength(50, ErrorMessage = "File Type cannot exceed 50 characters.")]
  
        public string FileType { get; set; }

        [Required(ErrorMessage = "File Size is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "File Size must be a positive number.")]
        public long FileSize { get; set; }
    }
}
