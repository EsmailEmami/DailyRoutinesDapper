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

        List<CategoriesListDTO> GetUserCategories(Guid userId, int skip, int take, string orderBy, string filter);
        List<CategoriesListDTO> GetUserRecycleCategories(Guid userId, int skip, int take, string orderBy, string filter);
        EditCategoryDTO GetCategoryForEdit(Guid categoryId);

        Category GetCategoryById(Guid categoryId);
        void DeleteCategory(Guid categoryId);
        Category AddCategory(Category category);
        void UpdateCategory(Category category);

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
        Action AddAction(Action action);
        void UpdateAction(Action action);
        void DeleteAction(Guid actionId);

        Action GetActionById(Guid actionId);

        bool IsCategoryExist(Guid categoryId);
        bool IsUserCategoryExist(Guid userId, Guid categoryId);
        bool IsUserActionExist(Guid userId, Guid actionId);

        CategoryDetailDTO GetCategoryDetail(Guid categoryId);
        CategoryDetailForAdminDTO GetCategoryDetailForAdmin(Guid categoryId);

        List<Action> GetActionsOfCategory(Guid categoryId);
    }
}