namespace DailyRoutines.Domain.DTOs.Paging
{
    public class BasePaging
    {
        public int PageId { get; set; } = 1;
        public int PageCount { get; set; }
        public int ActivePage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int TakeEntity { get; set; } = 10;
        public int SkipEntity { get; set; }
    }
}