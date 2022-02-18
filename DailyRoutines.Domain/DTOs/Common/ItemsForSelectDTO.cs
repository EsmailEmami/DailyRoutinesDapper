using System;

namespace DailyRoutines.Domain.DTOs.Common;

public class ItemsForSelectDTO
{
    public ItemsForSelectDTO(Guid value, string name)
    {
        Value = value;
        Name = name;
    }

    public Guid Value { get; set; }
    public string Name { get; set; }
}