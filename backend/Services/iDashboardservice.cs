using ModelEntity.MODEL;

namespace backend.Services
{
    public interface iDashboardservice
    {
        Task<IEnumerable<ChartDataDto>> GetBarChartData(int Employeeid, DateTime fromDate, DateTime ToDate);
        Task<IEnumerable<EmployeeName>> GetEmployeeNameLoad();
    }
}
