using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);

        Task<Company> GetCompanyAsync(Guid id, bool trackChanges);

        void CreateCompany(Company company);

        Task<IEnumerable<Company>> GetCompaniesByIdAsync(IEnumerable<Guid> ids, bool trackChanges);

        void DeleteCompany(Company company, bool trackChanges);
    }
}
