using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class CompanyForUpdateDto : CompanyBaseDto
    {
        //public string Name { get; set; }
        //public string Address { get; set; }
        //public string Country { get; set; }
        public IEnumerable<EmployeeForCreationDto> Employees { get; set; }
    }
}
