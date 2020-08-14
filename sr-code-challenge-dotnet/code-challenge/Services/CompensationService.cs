using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;
    

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        public Compensation GetById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {

                // _employeeRepository
                return _compensationRepository.GetById(id);
            }

            return null;
        }

        public Compensation Create(Compensation employeeCompensation)
        {

            if (employeeCompensation != null)
            {


                _compensationRepository.Add(employeeCompensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return employeeCompensation;
        }
    }
}
