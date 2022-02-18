using System;

namespace DailyRoutines.Domain.DTOs.Routine
{
    public class CategoriesListDTO
    {
        public CategoriesListDTO(Guid categoryId, string categoryTitle, string lastUpdateDate)
        {
            CategoryId = categoryId;
            CategoryTitle = categoryTitle;
            LastUpdateDate = lastUpdateDate;
        }

        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string LastUpdateDate { get; set; }
    }
}