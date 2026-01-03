using backend.MODEL;
using ModelEntity.MODEL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public interface iDashboardRepository
    {
        Task<IEnumerable<ChartDataDto>> GetBarChartData(int Employeeid, DateTime fromDate, DateTime ToDate);
        Task<IEnumerable<EmployeeName>> GetEmployeeNameLoad();
    }
}
