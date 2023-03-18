using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class ResumeModel
    {
        //Properties
        public int ID { get; set; }
        [DisplayName("Jobbtitel")]
        [Required(ErrorMessage = "Vänligen ange en jobbtitel")]
        public string? JobTitle { get; set; }
        [DisplayName("Företag")]
        [Required(ErrorMessage = "Vänligen ange företag")]
        public string? Company { get; set; }
        [DisplayName("Beskrivning")]
        [Required(ErrorMessage = "Vänligen ange en beskrivning")]
        public string? Description { get; set; }
        [DisplayName("Startår")]
        [Required(ErrorMessage = "Vänligen ange ett startår")]
        public int? YearStart { get; set; }
        [DisplayName("Slutår (om pågående, lämna tomt)")]
        public int? YearEnd { get; set; }
        [DisplayName("Pågående")]
        [Required(ErrorMessage = "Vänligen ange om pågående")]
        public bool? IsOngoing { get; set; }
    }
}
