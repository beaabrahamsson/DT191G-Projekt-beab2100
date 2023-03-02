namespace Portfolio.Models
{
    public class CVModel
    {
        //Properties
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? YearStart { get; set; }
        public int? YearEnd { get; set; }
        public string? MonthStart { get; set; }
        public string? MonthEnd { get; set; }
    }
}
