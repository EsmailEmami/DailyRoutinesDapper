using DailyRoutines.Domain.DTOs.Paging;
using System.Collections.Generic;

namespace DailyRoutines.Domain.DTOs.User;

public class FilterUsersDTO : BasePaging
{
    public string Search { get; set; }
    public string Type { get; set; } // all, active, blocked

    public List<UsersListDTO> Items { get; set; }

    public FilterUsersDTO SetPaging(BasePaging paging)
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

    public FilterUsersDTO SetItems(List<UsersListDTO> items)
    {
        Items = items;

        return this;
    }
}