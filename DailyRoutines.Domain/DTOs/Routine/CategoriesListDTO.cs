using System;

namespace DailyRoutines.Domain.DTOs.Routine
{
    public class CategoriesListDTO
    {
        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string LastUpdateDate { get; set; }
    }
}