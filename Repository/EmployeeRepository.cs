using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Employee>> GetEmployeesForCompanyAsync(Guid companyId, EmployeeParameters empParams, bool trackChanges)
        {
            var employees = await FindByCondition(e => e.CompanyId.Equals(companyId),trackChanges)
                    .FilterEmployees(empParams.MinAge, empParams.MaxAge)
                    .Search(empParams.SearchTerm)
                    .Sort(empParams.OrderBy)
                    .ToListAsync();
            
            return PagedList<Employee>
                .ToPagedList(employees, empParams.PageNumber, empParams.PageSize);
        }

        //public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackhanges) =>
        //    await FindByCondition(e => e.CompanyId.Equals(companyId), trackhanges).OrderBy(n => n.Name).ToListAsync();

        public async Task<Employee> GetEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges) =>
            await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Guid companyId, Employee employee) => Delete(employee);

    }
}
