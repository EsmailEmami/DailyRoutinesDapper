using DailyRoutines.Domain.DTOs.Paging;
using System;
using System.Collections.Generic;

namespace DailyRoutines.Domain.DTOs.Routine;

public class FilterActionsDTO : BasePaging
{
    public Guid CategoryId { get; set; }

    public string Search { get; set; }

    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }


    public List<ActionsListDTO> Items { get; set; }

    public FilterActionsDTO SetPaging(BasePaging paging)
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

    public FilterActionsDTO SetItems(List<ActionsListDTO> items)
    {
        Items = items;

        return this;
    }
}