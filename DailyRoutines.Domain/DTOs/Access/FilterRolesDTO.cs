using DailyRoutines.Domain.DTOs.Paging;
using System.Collections.Generic;

namespace DailyRoutines.Domain.DTOs.Access;

public class FilterRolesDTO : BasePaging
{
    public string Search { get; set; }

    public List<RolesListDTO> Items { get; set; }

    public FilterRolesDTO SetPaging(BasePaging paging)
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

    public FilterRolesDTO SetItems(List<RolesListDTO> items)
    {
        Items = items;

        return this;
    }
}