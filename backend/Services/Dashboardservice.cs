using backend.Repositories;
using backend.MODEL;
using ModelEntity.MODEL;

namespace backend.Services
{
    public class Dashboardservice : iDashboardservice
    {
        private readonly iDashboardRepository _DashboardRepository;

        public Dashboardservice(iDashboardRepository DashboardRepository)
        {
            _DashboardRepository = DashboardRepository;
        }

        public async Task<IEnumerable<ChartDataDto>> GetBarChartData(int Employeeid, DateTime fromDate, DateTime ToDate)
        {
            return await _DashboardRepository.GetBarChartData(Employeeid,fromDate,ToDate);
        }
        
                public async Task<IEnumerable<EmployeeName>> GetEmployeeNameLoad()
        {
            return await _DashboardRepository.GetEmployeeNameLoad();
        }


    }
}
