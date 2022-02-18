namespace DailyRoutines.Domain.DTOs.Routine;

public class ActionsMonthDTO
{
    public ActionsMonthDTO(int month, string title, int actionsCount)
    {
        Month = month;
        Title = title;
        ActionsCount = actionsCount;
    }

    public int Month { get; set; }
    public string Title { get; set; }
    public int ActionsCount { get; set; }
}