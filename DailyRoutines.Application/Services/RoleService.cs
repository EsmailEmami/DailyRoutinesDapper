using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.Interfaces;

namespace DailyRoutines.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _role;

        public RoleService(IRoleRepository role)
        {
            _role = role;
        }
    }
}