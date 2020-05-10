using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateCompany(Company company) => Create(company);

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                    .OrderBy(n => n.Name)
                    .ToListAsync();

        public async Task<Company> GetCompanyAsync(Guid id, bool trackChanges) => 
                await FindByCondition(a => a.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
                //await FindByCondition(a => a.Id == id, trackChanges).SingleOrDefaultAsync();

        public async Task<IEnumerable<Company>> GetCompaniesByIdAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(e => ids.Contains(e.Id), trackChanges).ToListAsync();

        public void DeleteCompany(Company company, bool trackChanges) => Delete(company);
    }
}
