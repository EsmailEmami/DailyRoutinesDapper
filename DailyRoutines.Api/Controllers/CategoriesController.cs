using DailyRoutines.Application.Common;
using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Application.Security;
using DailyRoutines.Domain.DTOs.Routine;
using DailyRoutines.Domain.Entities.Routine;
using DailyRoutines.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DailyRoutines.Api.Controllers;

[Access]
public class CategoriesController : SiteBaseController
{
    #region constructor

    private readonly IRoutineService _routineService;

    public CategoriesController(IRoutineService routineService)
    {
        _routineService = routineService;
    }

    #endregion

    #region Add Category

    [HttpPost("[action]")]
    public IActionResult AddCategory([FromBody] AddCategoryDTO categoryData)
    {
        if (!ModelState.IsValid)
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var category = new UserCategory()
        {
            CategoryTitle = categoryData.CategoryTitle,
            CreateDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
            UserId = User.GetUserId(),
            IsDelete = false
        };

        var addCategory = _routineService.AddCategory(category);

        if (addCategory == ResultTypes.Successful)
            return JsonResponseStatus.Success();


        return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");
    }

    #endregion

    #region Edit Category

    [HttpGet("[action]")]
    public IActionResult EditCategory([FromQuery] Guid categoryId)
    {
        if (!_routineService.IsUserCategoryExist(User.GetUserId(), categoryId))
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var category = _routineService.GetCategoryForEdit(categoryId);

        if (category == null)
            return JsonResponseStatus.NotFound("دسته بندی یافت نشد.");


        return JsonResponseStatus.Success(category);
    }

    [HttpPut("[action]")]
    public IActionResult EditCategory([FromBody] EditCategoryDTO categoryData)
    {
        if (!ModelState.IsValid)
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        if (!_routineService.IsUserCategoryExist(User.GetUserId(), categoryData.CategoryId))
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var category = _routineService.GetCategoryById(categoryData.CategoryId);

        if (category == null)
            return JsonResponseStatus.NotFound("دسته بندی یافت نشد.");

        category.CategoryTitle = categoryData.CategoryTitle;

        var editCategory = _routineService.EditCategory(category);

        if (editCategory == ResultTypes.Successful)
            return JsonResponseStatus.Success();


        return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");
    }

    #endregion

    #region Delete Category

    [HttpDelete("[action]")]
    public IActionResult DeleteCategory([FromQuery] Guid categoryId)
    {
        if (!_routineService.IsUserCategoryExist(User.GetUserId(), categoryId))
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var deleteCategory = _routineService.DeleteCategory(categoryId);

        if (deleteCategory == ResultTypes.Successful)
            return JsonResponseStatus.Success();

        return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");
    }

    #endregion

    #region Remove Category

    [HttpPut("[action]")]
    public IActionResult RemoveCategory([FromQuery] Guid categoryId)
    {
        if (!_routineService.IsUserCategoryExist(User.GetUserId(), categoryId))
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var category = _routineService.GetCategoryById(categoryId);

        if (category == null)
            return JsonResponseStatus.NotFound("دسته بندی یافت نشد.");

        category.IsDelete = true;

        var editCategory = _routineService.EditCategory(category);

        if (editCategory == ResultTypes.Successful)
            return JsonResponseStatus.Success();


        return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");
    }

    #endregion

    #region Return Category

    [HttpPut("[action]")]
    public IActionResult ReturnCategory([FromQuery] Guid categoryId)
    {
        if (!_routineService.IsUserCategoryExist(User.GetUserId(), categoryId))
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var category = _routineService.GetCategoryById(categoryId);

        if (category == null)
            return JsonResponseStatus.NotFound("دسته بندی یافت نشد.");

        category.IsDelete = false;

        var editCategory = _routineService.EditCategory(category);

        if (editCategory == ResultTypes.Successful)
            return JsonResponseStatus.Success();


        return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");
    }

    #endregion

    #region User Categories

    [HttpGet("[action]")]
    public IActionResult UserCategories([FromQuery] FilterCategoriesDTO filter)
    {
        filter.UserId = User.GetUserId();


        var categories = _routineService.GetCategories(filter);

        return categories == null ? JsonResponseStatus.NotFound() : JsonResponseStatus.Success(categories);
    }

    #endregion

    #region Recycle User Categories

    [HttpGet("[action]")]
    public IActionResult UserRecycleCategories([FromQuery] FilterCategoriesDTO filter)
    {
        filter.UserId = User.GetUserId();


        var categories = _routineService.GetRecycleCategories(filter);

        return categories == null ? JsonResponseStatus.NotFound() : JsonResponseStatus.Success(categories);
    }

    #endregion

    #region Category Detail

    [HttpGet("[action]")]
    public IActionResult CategoryDetail([FromQuery] Guid categoryId)
    {
        if (!_routineService.IsUserCategoryExist(User.GetUserId(), categoryId))
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var category = _routineService.GetCategoryDetail(categoryId);

        if (category == null)
            return JsonResponseStatus.NotFound();

        return JsonResponseStatus.Success(category);
    }

    #endregion

    #region Category Actions Month List

    [HttpGet("[action]")]
    public IActionResult CategoryActionsMonths(Guid categoryId)
    {
        if (!_routineService.IsUserCategoryExist(User.GetUserId(), categoryId))
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var data = _routineService.GetMonthsOfCategoryActions(categoryId);

        if (!data.Any())
            return JsonResponseStatus.NotFound();

        return JsonResponseStatus.Success(data);
    }

    #endregion

    #region Get Category Actions Year

    [HttpGet("[action]")]
    public IActionResult CategoryActionsYear([FromQuery] Guid categoryId)
    {
        if (!_routineService.IsUserCategoryExist(User.GetUserId(), categoryId))
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var data = _routineService.GetYearsOfCategoryActions(categoryId);

        if (!data.Any())
            return JsonResponseStatus.NotFound("متاسفانه اطلاعاتی یافت نشد.");

        return JsonResponseStatus.Success(data);
    }

    #endregion

    #region Categories For Select

    [HttpGet("[action]")]
    public IActionResult UserCategoriesForSelect()
    {
        var data = _routineService.GetUserCategoriesForSelect(User.GetUserId());

        if (!data.Any())
            return JsonResponseStatus.NotFound("متاسفانه اطلاعاتی یافت نشد.");

        return JsonResponseStatus.Success(data);
    }

    #endregion
}