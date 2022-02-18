using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.Interfaces;

namespace DailyRoutines.Application.Services;

public class AccessService : IAccessService
{
    private readonly IAccessRepository _role;

    public AccessService(IAccessRepository role)
    {
        _role = role;
    }
}