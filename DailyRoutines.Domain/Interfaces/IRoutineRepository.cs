using DailyRoutines.Domain.DTOs.Routine;
using System;
using System.Collections.Generic;
using DailyRoutines.Domain.DTOs.Common;
using DailyRoutines.Domain.Entities.Routine;
using Action = DailyRoutines.Domain.Entities.Routine.Action;

namespace DailyRoutines.Domain.Interfaces
{
    public interface IRoutineRepository
    {
        #region category

        FilterCategoriesDTO GetUserCategories(FilterCategoriesDTO filter);
        FilterCategoriesDTO GetUserRecycleCategories(FilterCategoriesDTO filter);
        EditCategoryDTO GetCategoryForEdit(Guid categoryId);

        UserCategory GetCategoryById(Guid categoryId);
        void RemoveCategory(UserCategory category);
        void AddCategory(UserCategory category);
        void UpdateCategory(UserCategory category);

        List<ItemsForSelectDTO> GetUserCategoriesForSelect(Guid userId);

        Guid GetUserIdOfCategory(Guid categoryId);

        #endregion



        FilterUserLastActionsDTO GetLastUserActions(FilterUserLastActionsDTO filter);
        FilterActionsDTO GetActionsOfCategory(FilterActionsDTO filter);

        List<DatesOfCategoryActionsDTO> GetActionsMonthOfCategory(Guid categoryId, int year);

        List<int> GetYearsOfCategoryActions(Guid categoryId);
        List<int> GetYearsOfActions(Guid userId);

        ActionDetailDTO GetActionDetail(Guid actionId);
        EditActionDTO GetActionForEdit(Guid actionId);
        void AddAction(Action action);
        void UpdateAction(Action action);
        void RemoveAction(Action action);

        Action GetActionById(Guid actionId);

        bool IsUserCategoryExist(Guid userId, Guid categoryId);
        bool IsUserActionExist(Guid userId, Guid actionId);

        CategoryDetailDTO GetCategoryDetail(Guid categoryId);
        CategoryDetailForAdminDTO GetCategoryDetailForAdmin(Guid categoryId);

        List<Action> GetActionsOfCategory(Guid categoryId);

        void SaveChanges();
    }
}