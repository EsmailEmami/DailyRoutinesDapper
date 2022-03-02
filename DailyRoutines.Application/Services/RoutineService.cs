using DailyRoutines.Application.Convertors;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.DTOs.Routine;
using DailyRoutines.Domain.Enums;
using DailyRoutines.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using DailyRoutines.Application.Extensions;
using DailyRoutines.Domain.DTOs.Common;
using DailyRoutines.Domain.Entities.Routine;
using Action = DailyRoutines.Domain.Entities.Routine.Action;

namespace DailyRoutines.Application.Services;

public class RoutineService : IRoutineService
{
    private readonly IRoutineRepository _routine;

    public RoutineService(IRoutineRepository routine)
    {
        _routine = routine;
    }


    public FilterCategoriesDTO GetCategories(FilterCategoriesDTO filter) =>
        _routine.GetUserCategories(filter);

    public FilterCategoriesDTO GetRecycleCategories(FilterCategoriesDTO filter) =>
        _routine.GetUserRecycleCategories(filter);

    public FilterActionsDTO GetActionsOfCategory(FilterActionsDTO filter) =>
        _routine.GetActionsOfCategory(filter);

    public FilterUserLastActionsDTO GetLastUserActions(FilterUserLastActionsDTO filter) =>
        _routine.GetLastUserActions(filter);

    public ResultTypes EditCategory(Category category)
    {
        try
        {
            _routine.UpdateCategory(category);

            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }

    public Category GetCategoryById(Guid categoryId) =>
        _routine.GetCategoryById(categoryId);

    public Guid GetUserIdOfCategory(Guid categoryId) =>
        _routine.GetUserIdOfCategory(categoryId);

    public ResultTypes AddAction(Action action)
    {
        try
        {
            _routine.AddAction(action);

            var category = _routine.GetCategoryById(action.CategoryId);

            if (category == null) 
                return ResultTypes.Failed;


            category.LastUpdateDate = DateTime.Now;

            _routine.UpdateCategory(category);


            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }

    public ResultTypes EditAction(Action action)
    {
        try
        {
            _routine.UpdateAction(action);

            var category = _routine.GetCategoryById(action.CategoryId);

            if (category != null)
            {
                category.LastUpdateDate = DateTime.Now;

                _routine.UpdateCategory(category);
            }

            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }

    public ResultTypes DeleteAction(Action action)
    {
        try
        {
            var category = _routine.GetCategoryById(action.CategoryId);

            if (category != null)
            {
                category.LastUpdateDate = DateTime.Now;

                _routine.UpdateCategory(category);
            }

            _routine.DeleteAction(action.ActionId);

            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }

    public EditActionDTO GetActionForEdit(Guid actionId) =>
        _routine.GetActionForEdit(actionId);

    public EditCategoryDTO GetCategoryForEdit(Guid categoryId) =>
        _routine.GetCategoryForEdit(categoryId);

    public ResultTypes AddCategory(Category category)
    {
        try
        {
            _routine.AddCategory(category);

            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }

    public ResultTypes DeleteCategory(Guid categoryId)
    {
        try
        {
            if (!_routine.IsCategoryExist(categoryId))
                return ResultTypes.Failed;

            _routine.DeleteCategory(categoryId);

            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }

    public ActionDetailDTO GetActionDetail(Guid actionId) =>
        _routine.GetActionDetail(actionId);

    public Action GetActionById(Guid actionId) =>
        _routine.GetActionById(actionId);

    public bool IsUserCategoryExist(Guid userId, Guid categoryId) =>
        _routine.IsUserCategoryExist(userId, categoryId);

    public bool IsUserActionExist(Guid userId, Guid actionId) =>
        _routine.IsUserActionExist(userId, actionId);

    public CategoryDetailDTO GetCategoryDetail(Guid categoryId)
    {
        return categoryId.IsEmpty() ? null : _routine.GetCategoryDetail(categoryId);
    }

    public CategoryDetailForAdminDTO GetCategoryDetailForAdmin(Guid categoryId) =>
        _routine.GetCategoryDetailForAdmin(categoryId);

    public ActionsDateFilter GetActionsMonthOfCategory(Guid categoryId, int year)
    {
        var actionsMonth = _routine.GetActionsMonthOfCategory(categoryId, year);

        if (!actionsMonth.Any())
            return null;

        return new ActionsDateFilter()
        {
            Year = year,
            Months = actionsMonth.Select(c => new ActionsMonthDTO(
                c.Value,
                c.Value.ToPersianMonthString(),
                c.ActionsCount
                )).ToList()
        };
    }

    public List<ActionsDateFilter> GetMonthsOfCategoryActions(Guid categoryId)
    {
        var years = _routine.GetYearsOfCategoryActions(categoryId);

        List<ActionsDateFilter> months = new List<ActionsDateFilter>();

        foreach (var year in years)
        {
            var actionsMonth = _routine.GetActionsMonthOfCategory(categoryId, year);

            if (actionsMonth.Any())
            {
                months.Add(new ActionsDateFilter()
                {
                    Year = year,
                    Months = actionsMonth.Select(c => new ActionsMonthDTO(
                        c.Value,
                        c.Value.ToPersianMonthString(),
                        c.ActionsCount
                    )).ToList()
                });
            }
        }

        months = months.OrderByDescending(c => c.Year).ToList();


        return months;
    }

    public List<int> GetYearsOfCategoryActions(Guid categoryId) =>
        _routine.GetYearsOfCategoryActions(categoryId);

    public List<int> GetYearsOfActions(Guid userId) =>
        _routine.GetYearsOfActions(userId);

    public List<ItemsForSelectDTO> GetUserCategoriesForSelect(Guid userId) =>
        _routine.GetUserCategoriesForSelect(userId);
}
