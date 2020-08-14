using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;
using challenge.Services;

namespace challenge.Repositories
{
    public class CompensationRespository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;
        private readonly IEmployeeService _employeeService;
        public CompensationRespository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext, EmployeeContext employeeContext, IEmployeeService employeeService)
        {
            _compensationContext = compensationContext;
            _employeeContext = employeeContext;
            _employeeService = employeeService;
            _logger = logger;
        }

        public Compensation Add(Compensation employeeCompensation)
        {
            if (employeeCompensation.employee.EmployeeId != null)
            {
                var existingEmployee = _employeeService.GetById(employeeCompensation.employee.EmployeeId);
                if (existingEmployee == null)
                {
                    string eID = Guid.NewGuid().ToString();
                    employeeCompensation.employee.EmployeeId = eID;
                    employeeCompensation.employeeID = eID;
                } 
                else
                {
                    employeeCompensation.employeeID = existingEmployee.EmployeeId;
                    _employeeService.Replace(existingEmployee, employeeCompensation.employee);
                }

               
            }

            _compensationContext.Compensations.Add(employeeCompensation);
            return employeeCompensation;
        }
        public Compensation GetById(string id)
        {

            //force employee list to enumerate. 

            _compensationContext.Compensations.ToArray(); 

            return _compensationContext.Compensations.SingleOrDefault(c => c.employeeID == id);
        }

        public Task SaveAsync()
        {

            return _compensationContext.SaveChangesAsync();
        }
    }
}
