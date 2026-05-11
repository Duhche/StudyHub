using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudyHub.Application.DTOs.Courses
{
    public class CreateCourseDto
    {
        
        [Required]
        [MinLength(3)]
        public string Title { get; set; } = null!;
    }
}
