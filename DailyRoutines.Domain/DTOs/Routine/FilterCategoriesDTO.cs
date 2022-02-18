using DailyRoutines.Domain.DTOs.Paging;
using System;
using System.Collections.Generic;

namespace DailyRoutines.Domain.DTOs.Routine;

public class FilterCategoriesDTO : BasePaging
{
    public Guid UserId { get; set; }
    public string Search { get; set; }

    public string OrderBy { get; set; }

    public List<CategoriesListDTO> Items { get; set; }

    public FilterCategoriesDTO SetPaging(BasePaging paging)
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

    public FilterCategoriesDTO SetItems(List<CategoriesListDTO> items)
    {
        Items = items;

        return this;
    }
}
