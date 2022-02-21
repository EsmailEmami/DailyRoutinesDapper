using DailyRoutines.Application.Common;
using DailyRoutines.Application.Convertors;
using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.DTOs.Routine;
using DailyRoutines.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Action = DailyRoutines.Domain.Entities.Routine.Action;

namespace DailyRoutines.Api.Controllers;

public class ActionsManagerController : SiteBaseController
{
    #region constrcuctor

    private readonly IRoutineService _routineService;

    public ActionsManagerController(IRoutineService routineService)
    {
        _routineService = routineService;
    }

    #endregion

    #region Get Category Actions

    [HttpGet("[action]")]
    public IActionResult CategoryActions([FromQuery] FilterActionsDTO filter)
    {
        if (filter.CategoryId.IsEmpty())
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var actions = _routineService.GetActionsOfCategory(filter);

        if (actions == null)
            return JsonResponseStatus.NotFound("متاسفانه اطلاعاتی یافت نشد.");

        return JsonResponseStatus.Success(actions);
    }

    #endregion


    #region Get Last Actions

    [HttpGet("[action]")]
    public IActionResult UserLastActions([FromQuery] FilterUserLastActionsDTO filter)
    {
        if (filter.UserId.IsEmpty())
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var actions = _routineService.GetLastUserActions(filter);

        if (actions == null)
            return JsonResponseStatus.NotFound("متاسفانه اطلاعاتی یافت نشد.");

        return JsonResponseStatus.Success(actions);
    }

    #endregion

    #region Action Years

    [HttpGet("[action]")]
    public IActionResult YearsOfActions([FromQuery] Guid userId)
    {
        if (userId.IsEmpty())
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");


        var data = _routineService.GetYearsOfActions(userId);

        return !data.Any() ? JsonResponseStatus.NotFound("متاسفانه اطلاعاتی یافت نشد.") :
            JsonResponseStatus.Success(data);
    }

    #endregion

    #region New Action

    [HttpPost("[action]")]
    public IActionResult AddAction([FromBody] AddActionDTO action)
    {
        if (!ModelState.IsValid)
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");


        var newAction = new Action()
        {
            CreateDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
            ActionTitle = action.ActionTitle,
            ActionDescription = action.ActionDescription,
            CreatePersianYear = DateTime.Now.ToPersianYear(),
            CreatePersianMonth = DateTime.Now.ToPersianMonth(),
            CreatePersianDay = DateTime.Now.ToPersianDay(),
            UserCategoryId = action.UserCategoryId
        };


        var addAction = _routineService.AddAction(newAction);

        if (addAction == ResultTypes.Failed)
            return JsonResponseStatus.Error(new
            {
                message = "مشکلی پیش آمده است. لطفا دوباره تلاش کنید."
            });


        return JsonResponseStatus.Success();
    }

    #endregion

    #region Edit Action

    [HttpGet("[action]")]
    public IActionResult EditAction([FromQuery] Guid actionId)
    {
        if (actionId.IsEmpty())
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var action = _routineService.GetActionForEdit(actionId);

        if (action == null)
            return NotFound("متاسفانه فعالیت یافت نشد.");

        return JsonResponseStatus.Success(action);
    }


    [HttpPut("[action]")]
    public IActionResult EditAction([FromBody] EditActionDTO action)
    {
        if (!ModelState.IsValid)
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var defaultAction = _routineService.GetActionById(action.ActionId);

        defaultAction.ActionTitle = action.ActionTitle;
        defaultAction.ActionDescription = action.ActionDescription;
        defaultAction.LastUpdateDate = DateTime.Now;


        var editAction = _routineService.EditAction(defaultAction);

        if (editAction == ResultTypes.Failed)
            return JsonResponseStatus.Error(new
            {
                message = "مشکلی پیش آمده است. لطفا دوباره تلاش کنید."
            });


        return JsonResponseStatus.Success();
    }

    #endregion

    #region Action Detail

    [HttpGet("[action]")]
    public IActionResult ActionDetail([FromQuery] Guid actionId)
    {
        if (actionId.IsEmpty())
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");


        var actionDetail = _routineService.GetActionDetail(actionId);

        if (actionDetail == null)
            return JsonResponseStatus.NotFound("متاسفانه فعالیت یافت نشد.");

        return JsonResponseStatus.Success(actionDetail);
    }

    #endregion

    #region Delete Action

    [HttpDelete("[action]")]
    public IActionResult DeleteAction([FromQuery] Guid actionId)
    {
        if (actionId.IsEmpty())
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var action = _routineService.GetActionById(actionId);

        if (action == null)
            return JsonResponseStatus.NotFound("متاسفانه فعالیت یافت نشد.");


        var editAction = _routineService.DeleteAction(action);

        if (editAction == ResultTypes.Failed)
            return JsonResponseStatus.Error("مشکلی پیش آمده است. لطفا دوباره تلاش کنید.");


        return JsonResponseStatus.Success();
    }

    #endregion
}
