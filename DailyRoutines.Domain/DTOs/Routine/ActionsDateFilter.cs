using System.Collections.Generic;

namespace DailyRoutines.Domain.DTOs.Routine;

public class ActionsDateFilter
{
    public int Year { get; set; }

    public List<ActionsMonthDTO> Months { get; set; }
}