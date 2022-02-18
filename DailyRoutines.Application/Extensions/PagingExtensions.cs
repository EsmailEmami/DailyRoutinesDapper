using DailyRoutines.Domain.DTOs.Paging;
using System.Linq;


namespace DailyRoutines.Application.Extensions;

public static class PagingExtensions
{
    public static IQueryable<T> Paging<T>(this IQueryable<T> queryable, BasePaging pager)
    {
        return queryable.Skip(pager.SkipEntity).Take(pager.TakeEntity);
    }
}

