using backend.Data;
using backend.MODEL;
using Microsoft.EntityFrameworkCore;
 
using ModelEntity.MODEL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public class DashboardRepository : iDashboardRepository
    {
        private readonly AppDbContext _dashboardcontext;

        public  DashboardRepository(AppDbContext dashboardcontext)
        {
            _dashboardcontext = dashboardcontext;
        }

        public async Task<IEnumerable<ChartDataDto>> GetBarChartData(int Employeeid, DateTime fromDate, DateTime ToDate)
        {
            return await _dashboardcontext.ChartDataDto.
                Where(x => 
                //x.id == Employeeid &&
                x.Fromdate >= fromDate.Date &&
                x.Todate <= ToDate.Date).
                Select(x=> new ChartDataDto
                { 
                Label = x.Label,
                Value = x.Value,
                })
                .AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<EmployeeName>> GetEmployeeNameLoad()
        {
            return await _dashboardcontext.EmployeeName.AsNoTracking().ToListAsync();
        }
    }
}
