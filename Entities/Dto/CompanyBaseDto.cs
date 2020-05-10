using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dto
{
    public abstract class CompanyBaseDto
    {
        [Required(ErrorMessage = "Company Name is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Company Name is 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company Address is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Company Address is 60 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Company Address Country is a required field")]
        [MaxLength(15, ErrorMessage = "Maximum length for the Company Address Country is 15 characters")]
        public string Country { get; set; }
        //public string FullAddress { get; set; }

    }
}
