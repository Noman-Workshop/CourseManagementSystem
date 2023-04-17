using Models;
using Services.Common;

namespace Services.Employees;

public interface IEmployeeService : IService<Employee, Guid> {
}