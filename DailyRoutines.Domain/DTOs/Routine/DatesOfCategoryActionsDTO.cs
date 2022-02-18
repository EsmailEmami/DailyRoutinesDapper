namespace DailyRoutines.Domain.DTOs.Routine;

public class DatesOfCategoryActionsDTO
{
    public DatesOfCategoryActionsDTO(int value, int actionsCount)
    {
        Value = value;
        ActionsCount = actionsCount;
    }


    public int Value { get; set; }
    public int ActionsCount { get; set; }
}