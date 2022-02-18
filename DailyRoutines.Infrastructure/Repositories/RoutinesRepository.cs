using DailyRoutines.Application.Convertors;
using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Generator;
using DailyRoutines.Domain.DTOs.Routine;
using DailyRoutines.Domain.Entities.Routine;
using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using DailyRoutines.Domain.DTOs.Common;
using Microsoft.EntityFrameworkCore;
using Action = DailyRoutines.Domain.Entities.Routine.Action;

namespace DailyRoutines.Infrastructure.Repositories;

public class RoutinesRepository : IRoutineRepository
{
    private readonly DailyRoutinesDbContext _context;

    public RoutinesRepository(DailyRoutinesDbContext context)
    {
        _context = context;
    }


    public FilterCategoriesDTO GetUserCategories(FilterCategoriesDTO filter)
    {
        IQueryable<UserCategory> categoriesQuery = _context.UserCategories
            .Where(c => c.UserId == filter.UserId);

        if (!string.IsNullOrEmpty(filter.Search))
            categoriesQuery = categoriesQuery.Where(c => c.CategoryTitle.Contains(filter.Search));

        categoriesQuery = filter.OrderBy.Fixed() switch
        {
            "createdate" => categoriesQuery.OrderByDescending(c => c.CreateDate),
            "updatedate" => categoriesQuery.OrderByDescending(c => c.LastUpdateDate),
            _ => categoriesQuery
        };

        int pagesCount = (int)Math.Ceiling(categoriesQuery.Count() / (double)filter.TakeEntity);

        var pager = Pager.Build(pagesCount, filter.PageId, filter.TakeEntity);



        var categories = categoriesQuery
            .Select(c => new CategoriesListDTO(c.Id, c.CategoryTitle, c.LastUpdateDate.ToPersianDateTime()))
            .Paging(pager).ToList();

        return filter.SetItems(categories)
            .SetPaging(pager);
    }

    public FilterCategoriesDTO GetUserRecycleCategories(FilterCategoriesDTO filter)
    {
        IQueryable<UserCategory> categoriesQuery = _context.UserCategories
            .Where(c => c.UserId == filter.UserId && c.IsDelete == true)
            .IgnoreQueryFilters();

        if (!string.IsNullOrEmpty(filter.Search))
            categoriesQuery = categoriesQuery.Where(c => c.CategoryTitle.Contains(filter.Search));

        categoriesQuery = filter.OrderBy.Fixed() switch
        {
            "createdate" => categoriesQuery.OrderByDescending(c => c.CreateDate),
            "updatedate" => categoriesQuery.OrderByDescending(c => c.LastUpdateDate),
            _ => categoriesQuery
        };

        int pagesCount = (int)Math.Ceiling(categoriesQuery.Count() / (double)filter.TakeEntity);

        var pager = Pager.Build(pagesCount, filter.PageId, filter.TakeEntity);

        var categories = categoriesQuery
            .Select(c => new CategoriesListDTO(c.Id, c.CategoryTitle, c.LastUpdateDate.ToPersianDateTime()))
            .Paging(pager).ToList();

        return filter.SetItems(categories)
            .SetPaging(pager);
    }

    public EditCategoryDTO GetCategoryForEdit(Guid categoryId) =>
        _context.UserCategories.Where(c => c.Id == categoryId)
            .Select(c => new EditCategoryDTO(c.Id, c.CategoryTitle))
            .SingleOrDefault();

    public UserCategory GetCategoryById(Guid categoryId) =>
        _context.UserCategories.IgnoreQueryFilters()
            .SingleOrDefault(c => c.Id == categoryId);

    public void RemoveCategory(UserCategory category) =>
        _context.UserCategories.Remove(category);

    public void AddCategory(UserCategory category) =>
        _context.UserCategories.Add(category);

    public void UpdateCategory(UserCategory category) =>
        _context.UserCategories.Update(category);

    public List<ItemsForSelectDTO> GetUserCategoriesForSelect(Guid userId) =>
        _context.UserCategories.Where(c => c.UserId == userId)
            .Select(c => new ItemsForSelectDTO(c.Id, c.CategoryTitle))
            .ToList();

    public FilterUserLastActionsDTO GetLastUserActions(FilterUserLastActionsDTO filter)
    {
        IQueryable<Action> actionsQuery = _context.Actions
            .Where(c => c.UserCategory.UserId == filter.UserId).
            OrderByDescending(c => c.CreateDate);

        if (filter.Year != 0)
        {
            actionsQuery = actionsQuery.Where(c => c.CreatePersianYear == filter.Year);
        }

        if (filter.Month != 0)
        {
            actionsQuery = actionsQuery.Where(c => c.CreatePersianMonth == filter.Month);
        }

        if (filter.Day != 0)
        {
            actionsQuery = actionsQuery.Where(c => c.CreatePersianDay == filter.Day);
        }

        if (!string.IsNullOrEmpty(filter.Search))
            actionsQuery = actionsQuery
                .Where(c => c.ActionTitle.Contains(filter.Search) ||
                            c.ActionDescription.Contains(filter.Search));


        int pagesCount = (int)Math.Ceiling(actionsQuery.Count() / (double)filter.TakeEntity);

        var pager = Pager.Build(pagesCount, filter.PageId, filter.TakeEntity);



        var actions = actionsQuery
            .Select(c => new ActionsListDTO(
                c.Id,
                c.ActionTitle,
                c.CreateDate.ToPersianDateTime()))
            .Paging(pager).ToList();

        return filter.SetItems(actions)
            .SetPaging(pager);
    }

    public FilterActionsDTO GetActionsOfCategory(FilterActionsDTO filter)
    {
        IQueryable<Action> actionsQuery = _context.Actions
            .Where(c => c.UserCategoryId == filter.CategoryId).
            OrderByDescending(c => c.CreateDate);

        if (filter.Year != 0)
        {
            actionsQuery = actionsQuery.Where(c => c.CreatePersianYear == filter.Year);
        }

        if (filter.Year != 0 && filter.Month != 0)
        {
            actionsQuery = actionsQuery.Where(c => c.CreatePersianYear == filter.Year &&
                                                   c.CreatePersianMonth == filter.Month);
        }

        if (filter.Year != 0 && filter.Month != 0 && filter.Day != 0)
        {
            actionsQuery = actionsQuery.Where(c => c.CreatePersianYear == filter.Year &&
                                                   c.CreatePersianMonth == filter.Month &&
                                                   c.CreatePersianDay == filter.Day);
        }

        if (!string.IsNullOrEmpty(filter.Search))
            actionsQuery = actionsQuery
                .Where(c => c.ActionTitle.Contains(filter.Search) ||
                            c.ActionDescription.Contains(filter.Search));


        int pagesCount = (int)Math.Ceiling(actionsQuery.Count() / (double)filter.TakeEntity);

        var pager = Pager.Build(pagesCount, filter.PageId, filter.TakeEntity);



        var actions = actionsQuery
            .Select(c => new ActionsListDTO(
                c.Id,
                c.ActionTitle,
                c.CreateDate.ToPersianDateTime()))
            .Paging(pager).ToList();

        return filter.SetItems(actions)
            .SetPaging(pager);
    }

    public List<DatesOfCategoryActionsDTO> GetActionsMonthOfCategory(Guid categoryId, int year) =>
        _context.Actions.Where(c => c.UserCategoryId == categoryId &&
                                    c.CreatePersianYear == year)
            .GroupBy(c => c.CreatePersianMonth)
            .Select(c => new DatesOfCategoryActionsDTO(c.Key, c.Count()))
            .ToList();

    public List<int> GetYearsOfCategoryActions(Guid categoryId) =>
        _context.Actions.Where(c => c.UserCategoryId == categoryId)
            .GroupBy(c => c.CreatePersianYear)
            .Select(c => c.Key)
            .ToList();

    public List<int> GetYearsOfActions(Guid userId) =>
        _context.Actions.Where(c => c.UserCategory.UserId == userId)
            .GroupBy(c => c.CreatePersianYear)
            .Select(c => c.Key)
            .ToList();


    public ActionDetailDTO GetActionDetail(Guid actionId) =>
        _context.Actions.Where(c => c.Id == actionId)
            .Select(c => new ActionDetailDTO(
                c.Id,
                c.ActionTitle,
                c.ActionDescription,
                c.CreateDate.ToPersianDateTime(),
                c.LastUpdateDate.ToPersianDateTime()
                ))
            .SingleOrDefault();

    public EditActionDTO GetActionForEdit(Guid actionId) =>
        _context.Actions.Where(c => c.Id == actionId)
            .Select(c => new EditActionDTO(c.Id, c.ActionTitle, c.ActionDescription))
            .SingleOrDefault();

    public void AddAction(Action action) => _context.Actions.Add(action);

    public void UpdateAction(Action action) => _context.Actions.Update(action);

    public void RemoveAction(Action action) => _context.Actions.Remove(action);
    public Action GetActionById(Guid actionId) => _context.Actions.Find(actionId);

    public bool IsUserCategoryExist(Guid userId, Guid categoryId) =>
        _context.UserCategories.IgnoreQueryFilters()
            .Any(c => c.Id == categoryId && c.UserId == userId);

    public bool IsUserActionExist(Guid userId, Guid actionId) =>
         _context.Actions.Any(c => c.Id == actionId &&
                                             c.UserCategory.UserId == userId);

    public CategoryDetailDTO GetCategoryDetail(Guid categoryId) =>
        _context.UserCategories.Where(c => c.Id == categoryId)
            .Select(c => new CategoryDetailDTO(
                c.Id,
                c.CategoryTitle,
                c.LastUpdateDate.ToPersianDateTime(),
                c.Actions.Count))
            .SingleOrDefault();

    public void SaveChanges() => _context.SaveChanges();
}
