using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.Interfaces;

namespace DailyRoutines.Application.Services
{
    public class UserCategoryService:IUserCategoryService
    {
        private readonly IUserCategoryRepository _userCategory;

        public UserCategoryService(IUserCategoryRepository userCategory)
        {
            _userCategory = userCategory;
        }
    }
}