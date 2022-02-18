using System;
using System.Collections.Generic;
using DailyRoutines.Domain.DTOs.Paging;

namespace DailyRoutines.Domain.DTOs.Routine;

public class FilterUserLastActionsDTO : BasePaging
{
    public Guid UserId { get; set; }

    public string Search { get; set; }

    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }


    public List<ActionsListDTO> Items { get; set; }

    public FilterUserLastActionsDTO SetPaging(BasePaging paging)
    {
        PageId = paging.PageId;
        PageCount = paging.PageCount;
        StartPage = paging.StartPage;
        EndPage = paging.EndPage;
        TakeEntity = paging.TakeEntity;
        SkipEntity = paging.SkipEntity;
        ActivePage = paging.ActivePage;

        return this;
    }

    public FilterUserLastActionsDTO SetItems(List<ActionsListDTO> items)
    {
        Items = items;

        return this;
    }
}