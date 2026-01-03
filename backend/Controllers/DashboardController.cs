using backend.MODEL;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelEntity.MODEL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{

    [ApiController]
    [Route("api/dashboard")]

    public class DashboardController : ControllerBase
    {
        private readonly iDashboardservice _Dashboardservice;

        public DashboardController(iDashboardservice Dashboardservice)
        {
            _Dashboardservice = Dashboardservice;
        }

        [HttpGet("chart-data")]
        public async Task<ActionResult<IEnumerable<ChartDataDto>>> GetBarchartData(int Employeeid, DateTime fromDate, DateTime ToDate)
        {
            //DateTime date = DateTime.Parse(fromDate);
            var BarchartData = await _Dashboardservice.GetBarChartData(Employeeid,fromDate,ToDate);
            return Ok(BarchartData);

        }

        [HttpGet("EmployeeNameLoad")]

        public async Task<ActionResult<IEnumerable<EmployeeName>>> GetEmployeeNameLoad()
        {
  var EmployeeNameDetais = await _Dashboardservice.GetEmployeeNameLoad();
            return Ok(EmployeeNameDetais);
        }
    }

}
