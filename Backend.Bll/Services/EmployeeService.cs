using Backend.Bll.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Bll.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDBContext _dbContext;
        public EmployeeService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<EmployeeDTO>> GetListAsync()
        {
            return await _dbContext.Employees.Select(e => new EmployeeDTO
            {
                ID = e.ID,
                Name = e.Name
            }).ToListAsync();
        }
        public async Task<EmployeeDTO> AddAsync(EmployeeDTO model)
        {
            var result = _dbContext.Employees.Add(new Models.Employee { Name = model.Name });
            await _dbContext.SaveChangesAsync();
            return new EmployeeDTO
            {
                Name = result.Entity.Name,
                ID = result.Entity.ID,
            };
        }
        public async Task<EmployeeDTO> GetAsync(int id)
        {
            var entity = await _dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
            if (entity == null)
                throw new ArgumentException($"Invalid employee id: {id}");

            return new EmployeeDTO
            {
                Name = entity.Name,
                ID = entity.ID
            };
        }
        public async Task<EmployeeDTO> EditAsync(EmployeeDTO model)
        {
            var entity = await _dbContext.Employees.FirstOrDefaultAsync(x => x.ID == model.ID);
            if (entity == null)
                throw new ArgumentException($"Invalid employee id: {model.ID}");

            entity.Name = model.Name;
            await _dbContext.SaveChangesAsync();
            return new EmployeeDTO
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Employees.FirstOrDefaultAsync(x => x.ID == id);
            if (entity == null)
                throw new ArgumentException($"Invalid employee id: {id}");

            _dbContext.Employees.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetListAsync();
        Task<EmployeeDTO> AddAsync(EmployeeDTO model);
        Task<EmployeeDTO> GetAsync(int id);
        Task<EmployeeDTO> EditAsync(EmployeeDTO model);
        Task DeleteAsync(int id);
    }
}
