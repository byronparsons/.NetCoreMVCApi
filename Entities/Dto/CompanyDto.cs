using System;

namespace Entities.Dto
{
    public class CompanyDto : CompanyBaseDto
    {
        public Guid Id { get; set; }
        //public string Name { get; set; }
        public string FullAddress { get; set; }

    }
}
