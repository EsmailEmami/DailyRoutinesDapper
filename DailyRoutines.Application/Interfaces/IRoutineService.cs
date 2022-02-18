using DailyRoutines.Domain.DTOs.Routine;
using DailyRoutines.Domain.Enums;
using System;
using System.Collections.Generic;
using DailyRoutines.Domain.DTOs.Common;
using DailyRoutines.Domain.Entities.Routine;
using Action = DailyRoutines.Domain.Entities.Routine.Action;

namespace DailyRoutines.Application.Interfaces;


public interface IRoutineService
{
    FilterCategoriesDTO GetCategories(FilterCategoriesDTO filter);
    FilterActionsDTO GetActionsOfCategory(FilterActionsDTO filter);
    FilterUserLastActionsDTO GetLastUserActions(FilterUserLastActionsDTO filter);
    EditCategoryDTO GetCategoryForEdit(Guid categoryId);
    ResultTypes AddCategory(UserCategory category);
    ResultTypes DeleteCategory(Guid categoryId);
    ResultTypes EditCategory(UserCategory category);
    UserCategory GetCategoryById(Guid categoryId);

    ResultTypes AddAction(Action action);
    ResultTypes EditAction(Action action);
    ResultTypes DeleteAction(Action action);
    EditActionDTO GetActionForEdit(Guid actionId);

    ActionDetailDTO GetActionDetail(Guid actionId);

    Action GetActionById(Guid actionId);

    bool IsUserCategoryExist(Guid userId, Guid categoryId);
    bool IsUserActionExist(Guid userId, Guid actionId);

    CategoryDetailDTO GetCategoryDetail(Guid categoryId);

    ActionsDateFilter GetActionsMonthOfCategory(Guid categoryId, int year);

    List<ActionsDateFilter> GetMonthsOfCategoryActions(Guid categoryId);
    List<int> GetYearsOfCategoryActions(Guid categoryId);
    List<int> GetYearsOfActions(Guid userId);

    List<ItemsForSelectDTO> GetUserCategoriesForSelect(Guid userId);
}
