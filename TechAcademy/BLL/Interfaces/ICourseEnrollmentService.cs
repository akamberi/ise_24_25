using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;



using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICourseEnrollmentService
    {
        Task<bool> EnrollStudentInCourseAsync(int courseId, string studentId);
    }
}
