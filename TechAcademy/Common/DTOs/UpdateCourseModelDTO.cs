using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
public class UpdateCourseModuleDto
{
    public string? Name { get; set; } = string.Empty;
    public string? Content { get; set; }  // Nullable content for updating
}

}
