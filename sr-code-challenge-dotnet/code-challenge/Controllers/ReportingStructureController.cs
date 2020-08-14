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
    [Route("api/reportingstructure")]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public IActionResult GetReportingStructureDataByEmployeeId(String id)
        {

            // find employee record

            _logger.LogDebug($"Received reporting structure get request for employee '{id}'");

            var employee = _employeeService.GetById(id);
            
            if (employee == null)
                return NotFound();

            // create ReportingStructure data from found employee record

            ReportingStructure rs = new ReportingStructure();
            rs.employee = employee;


            if (rs.employee.DirectReports == null)
            {
                rs.numberOfReports = 0;
            }
            else
            {
  

                List<Employee> allDirectReportsToEmployeeInTree = IEmumerationUtilities.Traverse(rs.employee).ToList();

                rs.numberOfReports = allDirectReportsToEmployeeInTree.Count - 1;  // subtract the root node from the count
            }

            // return the reporting structure data object

            return Ok(rs);


        }



    }
}

