using System.ComponentModel.DataAnnotations;

namespace Entities.Dto
{
    public abstract class EmployeeBaseDto
    {
        [Required(ErrorMessage = "Employee name is required.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is a required field")]
        [Range(18, int.MaxValue, ErrorMessage = "The age of Employee cannot be less than 18.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Position is a required field")]
        [MaxLength(20, ErrorMessage = "Maximum length of Position is 20 characters")]
        public string Position { get; set; }
    }
}
