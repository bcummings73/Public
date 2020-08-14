using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;
using challenge.Utilities;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

 
        [HttpGet("{id}", Name = "getEmployeeCompensationById")]
        public IActionResult GetEmployeeCompensationById(String id)
        {

            _logger.LogDebug($"Received employee compensation get request for '{id}'");

            var employeeCompensation = _compensationService.GetById(id);


            if (employeeCompensation == null)
                return NotFound();


            return Ok(employeeCompensation);
        }

        [HttpPost]
        public IActionResult CreateEmployeeCompensation([FromBody] Compensation compensation)
        {
           

 
              _logger.LogDebug($"Received employee compensation create request for '{compensation.employee.FirstName} {compensation.employee.LastName}'");

              _compensationService.Create(compensation);

              return CreatedAtRoute("getEmployeeCompensationById", new { id = compensation.employeeID}, compensation);
            

        }

    }
}

