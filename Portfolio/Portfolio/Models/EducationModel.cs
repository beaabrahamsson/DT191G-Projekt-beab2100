using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class EducationModel
    {
        //Properties
        public int ID { get; set; }
        [DisplayName("Titel")]
        [Required(ErrorMessage = "Vänligen ange en titel")]
        public string? Title { get; set; }
        [DisplayName("Skola")]
        [Required(ErrorMessage = "Vänligen ange en skola")]
        public string? School { get; set; }
        [DisplayName("Poäng")]
        [Required(ErrorMessage = "Vänligen ange antal poäng")]
        public int? Credits { get; set; }
        [DisplayName("Beskrivning")]
        [Required(ErrorMessage = "Vänligen ange en beskrivning")]
        public string? Description { get; set; }
        [DisplayName("Startår")]
        [Required(ErrorMessage = "Vänligen ange startår")]
        public int? YearStart { get; set; }
        [DisplayName("Slutår")]
        [Required(ErrorMessage = "Vänligen ange slutår")]
        public int? YearEnd { get; set; }
    }
}

